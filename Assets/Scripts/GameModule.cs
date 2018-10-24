using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModule : MonoBehaviour {

    public GameObject Track;
    public GameObject MenuObject;
    public GameObject boss;
    public GameObject wall;
    public AudioSource Audio;
    public AudioSource RhythmAudio;
    public AudioSource GuitarAudio;
    public GameObject CameraEye;
    public GameObject CameraRig;
    public AudioSource HitChallenge;
    public AudioSource Hit;
    public AudioSource portalHit;
    public GameObject controllerLeft;
    public GameObject controllerRight;
    public GameObject controllerMenu;
    public GameObject LeftStick;
    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject RightStick;
    public GameObject reminder;
    public GameObject pointsObject;
    public GameObject leftPortalText;
    public GameObject rightPortalText;
    public GameObject streakObject;
    public GameObject multiplyerObject;
    public uint streak = 0;
    public bool inCube = false;
    public bool isPlaying = false;
    public uint points = 0;
    public uint multiplier = 1;
    public uint noteTotal = 0;
    public uint hitTotal = 0;


    public void Vibrate(GameObject Controller, SteamVR_Controller.Device device)
    {
        device.TriggerHapticPulse(3900);
        device.TriggerHapticPulse(3900);
        device.TriggerHapticPulse(3900);
        device.TriggerHapticPulse(3900);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isPlaying)
            SetStreak();
    }

    void SetStreak()
    {
        if (streak >= 10 && streak <= 19)
            multiplier = 2;

        if (streak >= 20 && streak <= 29)
            multiplier = 4;

        if (streak >= 30 && streak <= 39)
            multiplier = 6;

        if (streak >= 40)
            multiplier = 8;

        if (streak <= 9)
            multiplier = 1;
    }
}
