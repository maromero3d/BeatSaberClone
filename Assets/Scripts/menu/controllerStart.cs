using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wacki;

public class controllerStart : MonoBehaviour {

    public GameObject hammer;
    public GameObject hammerPos;
    public GameObject controllerModel;
    public GameObject controller;
    bool inController = false;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        /*if (inController)
        {
            if (controller.GetComponent<SteamVR_TrackedController>().triggerPressed)
            {
                inController = false;
                hammer.transform.SetParent(controller.transform);
                hammer.transform.rotation = hammerPos.transform.rotation;
                hammer.transform.position = hammerPos.transform.position;
                var joint = AddFixedJoint();
                joint.connectedBody = hammer.GetComponent<Rigidbody>();
                hammer.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                hammer.gameObject.GetComponent<Rigidbody>().useGravity = false;
                hammer.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                controllerModel.SetActive(false);

                if (FiveNoteGame.playtrack)
                   return;

                if (hammer.name == "LeftHammer")
                    FiveNoteGame.hammerLeftStatus = "Ready";

                if (hammer.name == "RightHammer")
                    FiveNoteGame.hammerRightStatus = "Ready";

                
            }
        }     */   
    }
    

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = controller.AddComponent<FixedJoint>();
        fx.breakForce = Mathf.Infinity;
        fx.breakTorque = Mathf.Infinity;
        return fx;
    }

    void OnTriggerEnter(Collider other)
    {
        if (GameObject.Find("Menu"))
            return;

        if (other.name == "Model")
        {
            controllerModel = other.gameObject;
            inController = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Model")
            inController = false;
    }
}
