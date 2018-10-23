using UnityEngine;
using System.Collections.Generic;

/**
 * Lightsaber activate / deactivate script.
 * 
 * The blade on/off algorithm works by setting the scale of the blade. Therefore the pivot must be at the bottom of the blade. You can adapt that easily in Blender.
 * The scale is applied in z direction
 * 
 * Notes:
 * + light and blade color are initialized using the blade color property, the mk glow color and the light color will be overwritten
 * + blade color should have alpha set or you probably won't see any glowing
 * 
 */
public class Lightsaber : MonoBehaviour {

    [Tooltip("A list of lightsaber blade / blade game objects, each of them can have a light as child. The light's intensity depends on the blade length.")]
    public List<GameObject> bladeGameObjects;

    [Tooltip("Beam extend speed in seconds.")]
    public float bladeExtendSpeed = 0.3f;

    [Tooltip("Whether the saber (and all of its blades) is active initially or not.")]
    public bool saberActive = false;

    [Tooltip("The blade and light color. Alpha should be set, the higher alpha is, the bigger the glow effect. If alpha is 0, then there's no glow effect.")]
    public Color bladeColor;

    public AudioClip soundOn;
    public AudioClip soundOff;
    public AudioClip soundLoop;
    public AudioClip soundSwing;

    public AudioSource AudioSource;
    public AudioSource AudioSourceLoop;
    public AudioSource AudioSourceSwing;
    

    // swinging
    // TODO: make it depend on velocity of VR controller
    private float swingSpeed = 0;
    private Vector3 lastSwingPosition = Vector3.zero;

    /// <summary>
    /// Properties of a single blade.
    /// This way you can attach multiple blades to a lightsaber
    /// </summary>
    private class Blade
    {
        // the blade itself
        public GameObject gameObject;

        // the light attached to the blade
        public Light light;

        // minimum blade length
        private float scaleMin;

        // maximum blade length; initialized with length from scene
        private float scaleMax;

        // current scale, lerped between min and max scale
        private float scaleCurrent;

        public bool active = false;

        // the delta is a lerp value within 1 second. it will be initialized depending on the extend speed
        private float extendDelta;

        private float localScaleX;
        private float localScaleZ;

        public Blade( GameObject gameObject, float extendSpeed, bool active)
        {

            this.gameObject = gameObject;
            this.light = gameObject.GetComponentInChildren<Light>();
            this.active = active;

            // consistency check
            if (light == null)
            {
                Debug.Log("No light found. Blade should have a light as child");
            }

            // remember initial scale values (non extending part of the blade)
            this.localScaleX = gameObject.transform.localScale.x;
            this.localScaleZ = gameObject.transform.localScale.z;

            // remember initial scale values (extending part of the blade)
            this.scaleMin = 0f;
            this.scaleMax = gameObject.transform.localScale.y;

            // initialize variables
            // the delta is a lerp value within 1 second. depending on the extend speed we have to size it accordingly
            extendDelta = this.scaleMax / extendSpeed;

            if (active)
            {
                // set blade size to maximum
                scaleCurrent = scaleMax;
                extendDelta *= 1;
            }
            else
            {
                // set blade size to minimum
                scaleCurrent = scaleMin;
                extendDelta *= -1;
            }

        }

        public void SetActive( bool active)
        {
            // whether to scale in positive or negative direction
            extendDelta = active ? Mathf.Abs(extendDelta) : -Mathf.Abs(extendDelta);

        }

        public void SetColor( Color color)
        {
            if (light != null)
            {
                light.color = color;
            }

            // TODO: make fail-safe. accessing index 0 of materials and the fixed constant _MKGlowColor is risky
            gameObject.GetComponent<MeshRenderer>().materials[0].SetColor("_MKGlowColor", color);

        }

        public void updateLight()
        {
            if (this.light == null)
                return;

            // light intensity depending on blade size
            this.light.intensity = this.scaleCurrent;
        }

        public void updateSize()
        {

            // consider delta time with blade extension
            scaleCurrent += extendDelta * Time.deltaTime;

            // clamp blade size
            scaleCurrent = Mathf.Clamp(scaleCurrent, scaleMin, scaleMax);

            // scale in z direction
            gameObject.transform.localScale = new Vector3(this.localScaleX, scaleCurrent, this.localScaleZ);

            // whether the blade is active or not
            active = scaleCurrent > 0;
             
            // show / hide the gameobject depending on the blade active state
            if (active && !gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
            else if(!active && gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
            
        }
    }

    private List<Blade> blades;

    // Use this for initialization
    void Awake () {

        // consistency check
        if (bladeGameObjects.Count == 0) {
            Debug.LogError("No blades found. Must have at least 1 blade!");
        }

        // store initial attributes
        blades = new List<Blade>();
        foreach (GameObject bladeGameObject in bladeGameObjects)
        {
            blades.Add(new Blade(bladeGameObject, bladeExtendSpeed, saberActive));
        }


        // initialize audio depending on beam activitiy
        InitializeAudio();

        // light and blade color
        InitializeBladeColor();

        // initially update blade length, so that it isn't set to what we have in unity's visual editor
        UpdateBlades();


    }

    void InitializeAudio()
    {

        // initialize audio depending on beam activitiy
        if (saberActive)
        {
            AudioSourceLoop.clip = soundLoop;
            AudioSourceLoop.Play();
        }

    }

    // set the color of the light and the blade
    void InitializeBladeColor()
    {

        // check if alpha is set; if it isn't set, then there'll be no glow effect
        if (bladeColor.a == 0)
        {
            // Debug.Log("Beam color alpha is 0.");
            // bladeColor.a = 1;
        }

        // update blade color, light color and glow color
        foreach (Blade blade in blades)
        {
            blade.SetColor(bladeColor);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
        // key pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Debug.Log("Space was pressed");

            ToggleLightsaberOnOff();

        }

        UpdateBlades();


        // light and blade color
        // only for testing dynamic colors, works.
        // UpdateColor();

        // swing speed
        updateSwingHandler();


    }

    // calculate swing speed
    private void updateSwingHandler()
    {
        // calculate speed
        swingSpeed = (((transform.position - lastSwingPosition).magnitude) / Time.deltaTime);

        // remember last position
        lastSwingPosition = transform.position;

        // swing sound
        // TODO: a probably better solution would be to play the swing sound permanently and only fade the volume in and out depending on the swing speed
        if (saberActive) // TODO: consider scale, i. e bladeScaleCurrent == bladeScaleMax)
        {
            // Debug.Log("Swing speed: " + swingSpeed);

            // if certain swing speed is reached, play swing audio sound. if swinging stopped, fade the volume out
            if (swingSpeed > 0.8) // TODO: just random swing values; needs to me more generic
            {
                if (!AudioSourceSwing.isPlaying)
                {
                    AudioSourceSwing.volume = 1f;
                    AudioSourceSwing.PlayOneShot(soundSwing);
                }
            }
            else
            {

                // fade out volume
                if(AudioSourceSwing.isPlaying && AudioSourceSwing.volume > 0)
                {
                    AudioSourceSwing.volume *= 0.9f; // TODO: just random swing values; needs to me more generic
                }
                else
                {
                    AudioSourceSwing.volume = 0;
                    AudioSourceSwing.Stop();
                }

            }
        }
    }

    private void ToggleLightsaberOnOff()
    {
        if (saberActive)
        {
            LightsaberOff();
        }
        else
        {
            LightsaberOn();
        }

    }

    private void LightsaberOn()
    {
        foreach (Blade blade in blades)
        {
            blade.SetActive(true);
        }
            

        AudioSource.PlayOneShot(soundOn);
        AudioSourceLoop.clip = soundLoop;
        AudioSourceLoop.Play();

    }

    private void LightsaberOff()
    {
        foreach (Blade blade in blades)
        {
            blade.SetActive(false);
        }

        AudioSource.PlayOneShot(soundOff);
        AudioSourceLoop.Stop();

    }

    private void UpdateBlades()
    {
        foreach (Blade blade in blades)
        {

            blade.updateLight();
            blade.updateSize();

        }

        // saber is active if any of the blades is active
        bool active = false;
        foreach (Blade blade in blades)
        {
            if( blade.active)
            {
                active = true;
                break;
            }
           
        }

        this.saberActive = active;
    }

}
