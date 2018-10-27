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
    public GameObject LeftSaber;
    public GameObject LeftHandModel;
    public GameObject RightHandModel;
    public GameObject LeftBlade;
    public GameObject RightBlade;
    public GameObject RightSaber;
    public GameObject reminder;
    public GameObject pointsObject;
    public GameObject leftPortalText;
    public GameObject rightPortalText;
    public GameObject leftPos;
    public GameObject rightPos;
    public GameObject streakObject;
    public GameObject multiplyerObject;
    SteamVR_TrackedObject Lcontroller;
    SteamVR_TrackedObject Rcontroller;
    public uint streak = 0;
    public bool inCube = false;
    public bool isPlaying = false;
    public bool LcontrollerSet = false;
    public bool RcontrollerSet = false;
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

    public void setLeftSaber()
    {
        if (controllerLeft.GetComponent<FixedJoint>())
            if (controllerLeft.GetComponent<FixedJoint>().connectedBody != null)
                controllerLeft.GetComponent<FixedJoint>().connectedBody = null;

        LeftSaber.transform.rotation = leftPos.transform.rotation;
        LeftSaber.transform.position = leftPos.transform.position;

        var jointL = AddFixedJoint(controllerLeft);
        jointL.connectedBody = LeftSaber.GetComponent<Rigidbody>();

        LeftHandModel.SetActive(false);

        LcontrollerSet = true;
    }

    public void setRightSaber()
    {
        if (controllerRight.GetComponent<FixedJoint>())
            if (controllerRight.GetComponent<FixedJoint>().connectedBody != null)
                controllerRight.GetComponent<FixedJoint>().connectedBody = null;

        RightSaber.transform.rotation = rightPos.transform.rotation;
        RightSaber.transform.position = rightPos.transform.position;

        var jointR = AddFixedJoint(controllerRight);
        jointR.connectedBody = RightSaber.GetComponent<Rigidbody>();

        RightHandModel.SetActive(false);

        RcontrollerSet = true;
    }
    // Use this for initialization
    void Start () {
        Rcontroller = controllerRight.GetComponent<SteamVR_TrackedObject>();
        Lcontroller = controllerLeft.GetComponent<SteamVR_TrackedObject>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isPlaying)
            SetStreak();

        if (Rcontroller.isValid && !RcontrollerSet)
            setRightSaber();

        if (Lcontroller.isValid && !LcontrollerSet)
            setLeftSaber();

        if (!Lcontroller.isValid)
            LcontrollerSet = false;

        if (!Rcontroller.isValid)
            RcontrollerSet = false;
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

    private FixedJoint AddFixedJoint(GameObject obj)
    {
        FixedJoint fx = obj.AddComponent<FixedJoint>();
        fx.breakForce = Mathf.Infinity;
        fx.breakTorque = Mathf.Infinity;
        return fx;
    }
}
