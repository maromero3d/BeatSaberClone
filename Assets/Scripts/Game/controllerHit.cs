using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EzySlice;

public class controllerHit : MonoBehaviour {
    SteamVR_Controller.Device device;
    SteamVR_TrackedObject controller;
    public GameObject Controller;
    public Rigidbody trackedHand;
    public GameObject noForece;
    public GameObject noTrigger;
    public Material crossMat;
    public GameObject obg;
    string orange = "#FF8B00";
    Color greenGrey;
    Material myMaterial;
    GameModule game;
    // Use this for initialization
    void Start () {
        game = GameObject.Find("GameModule").GetComponent<GameModule>();
        controller = Controller.GetComponent<SteamVR_TrackedObject>();
        ColorUtility.TryParseHtmlString("#3E594CFF", out greenGrey);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(controller.isValid)
            device = SteamVR_Controller.Input((int)controller.index);
    }

    bool needTrigger = false;
    bool isTRiggered = false;


    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Box")
        {
            //if (game.inCube)
                //return;
                        
            Vector3 test = trackedHand.GetPointVelocity(transform.TransformPoint(other.transform.position));
            //if (device.velocity.x > 0.25 || device.velocity.y > 0.25 || device.velocity.z > 0.25 || device.angularVelocity.x > 0.25 || device.angularVelocity.y > 0.25 || device.angularVelocity.z > 0.25)
            //{
                game.inCube = true;

                _Sliced hull = other.gameObject.Slice(obg.transform.position, obg.transform.up, crossMat);

                if (hull != null)
                {
                    hull.CreateLowerHull(other.gameObject, crossMat);
                    hull.CreateUpperHull(other.gameObject, crossMat);
                }

                game.Hit.Play();
                game.Vibrate(Controller, device);

                //GameObject _explosion;
                game.streak += 1;
                game.hitTotal += 1;
                game.points += 50 * game.multiplier;
                //myMaterial = GetComponent<Renderer>().material;
                //myMaterial.SetColor("_EmissionColor", greenGrey);
                //parent.GetComponent<CubeScript>().notePosition;
                /*_explosion = Instantiate(Resources.Load("Prefabs/Spark" + parentName) as GameObject);
                _explosion.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                _explosion.transform.position = transform.position;
                _explosion.name = "Spark" + parentName;
                _explosion.AddComponent<Trash>();*/
                Destroy(other.gameObject.transform.parent.gameObject);
           // }
            /*else
            {
                GameObject temp = Instantiate(noForece);
                temp.transform.SetParent(GameObject.Find("gameText").transform);
                temp.transform.localPosition = new Vector3(0, 0, 0);
                temp.SetActive(true);
                game.streak = 0;
                temp.AddComponent<Trash>();
                temp.GetComponent<Trash>().isFloatingText = true;
            }*/

        }

    }

    void OnTriggerExit(Collider other)
    {
        game.inCube = false;
    }
}
