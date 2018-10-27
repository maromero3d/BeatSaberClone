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
    
    [Tooltip("The blade and light color. Alpha should be set, the higher alpha is, the bigger the glow effect. If alpha is 0, then there's no glow effect.")]
    public Color bladeColor;
        

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

        // the delta is a lerp value within 1 second. it will be initialized depending on the extend speed
        private float extendDelta;

        private float localScaleX;
        private float localScaleZ;

        public Blade(GameObject gameObject, float extendSpeed)
        {

            this.gameObject = gameObject;
            this.light = gameObject.GetComponentInChildren<Light>();

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

            scaleCurrent = scaleMax;
            extendDelta *= 1;

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
            blades.Add(new Blade(bladeGameObject, bladeExtendSpeed));
        }
        
        // light and blade color
        InitializeBladeColor();

        // initially update blade length, so that it isn't set to what we have in unity's visual editor
        UpdateBlades();


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
        
        UpdateBlades();
        updateSwingHandler();
    }

    // calculate swing speed
    private void updateSwingHandler()
    {
        // calculate speed
        swingSpeed = (((transform.position - lastSwingPosition).magnitude) / Time.deltaTime);

        // remember last position
        lastSwingPosition = transform.position;        
    }
    
    private void UpdateBlades()
    {
        foreach (Blade blade in blades)
        {

            blade.updateLight();
            blade.updateSize();

        }
    }

}
