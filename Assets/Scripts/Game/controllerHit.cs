using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class controllerHit : MonoBehaviour {
    SteamVR_Controller.Device device;
    SteamVR_TrackedObject controller;
    public GameObject Controller;
    public Rigidbody trackedHand;
    public GameObject noForece;
    public GameObject noTrigger;
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
        device = SteamVR_Controller.Input((int)controller.index);
    }

    bool needTrigger = false;
    bool isTRiggered = false;


    void OnTriggerEnter(Collider other)
    {
        if (other.name == "box")
        {
            if (game.inCube)
                return;

            isTRiggered = Controller.GetComponent<SteamVR_TrackedController>().triggerPressed;

            //if(other.gameObject.transform.parent.gameObject.name == "AnyRed")
                //if(other.gameObject.transform.parent.gameObject.GetComponent<barCord>().note.IsHammerOn)
                    //needTrigger = true;

            //if (other.gameObject.transform.parent.gameObject.name == "AnyBlue")
                //if (other.gameObject.transform.parent.gameObject.GetComponent<CubeScript>().note.IsHammerOn)
                    //needTrigger = true;

            if (needTrigger)
            {
                if (isTRiggered)
                {
                    needTrigger = false;
                }
                else
                {
                    needTrigger = false;
                    GameObject temp = Instantiate(noTrigger);
                    temp.transform.SetParent(GameObject.Find("gameText").transform);
                    temp.transform.localPosition = new Vector3(0, 0, 0);
                    temp.SetActive(true);
                    game.streak = 0;
                    temp.AddComponent<Trash>();
                    temp.GetComponent<Trash>().isFloatingText = true;
                    return;
                }
            }

            Vector3 test = trackedHand.GetPointVelocity(transform.TransformPoint(other.transform.position));
            if (device.velocity.x > 0.25 || device.velocity.y > 0.25 || device.velocity.z > 0.25 || device.angularVelocity.x > 0.25 || device.angularVelocity.y > 0.25 || device.angularVelocity.z > 0.25)
            {
                game.inCube = true;

                //if (SelectSong.isFiveNote)
                    //if (FiveNoteGame.makeHard)
                        //if (game.streak == 75)
                            //game.HitChallenge.Play();


                game.Hit.Play();
                game.Vibrate(Controller, device);

                GameObject _explosion;
                GameObject parent = other.gameObject.transform.parent.gameObject;
                string parentName = other.gameObject.transform.parent.gameObject.name;
                other.gameObject.transform.SetParent(parent.transform.parent);

                other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                other.gameObject.GetComponent<Rigidbody>().useGravity = true;
                other.gameObject.GetComponent<Rigidbody>().velocity = device.velocity * 4;
                other.gameObject.GetComponent<Rigidbody>().angularVelocity = device.angularVelocity * 4;
                other.gameObject.AddComponent<Trash>();
                game.streak += 1;
                game.hitTotal += 1;
                game.points += 50 * game.multiplier;
                //myMaterial = GetComponent<Renderer>().material;
                //myMaterial.SetColor("_EmissionColor", greenGrey);
                //parent.GetComponent<CubeScript>().notePosition;
                _explosion = Instantiate(Resources.Load("Prefabs/Spark" + parentName) as GameObject);
                _explosion.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                _explosion.transform.position = transform.position;
                _explosion.name = "Spark" + parentName;
                _explosion.AddComponent<Trash>();
                other.gameObject.name = "_box" + parentName;
                Destroy(parent);
            }
            else
            {
                GameObject temp = Instantiate(noForece);
                temp.transform.SetParent(GameObject.Find("gameText").transform);
                temp.transform.localPosition = new Vector3(0, 0, 0);
                temp.SetActive(true);
                game.streak = 0;
                temp.AddComponent<Trash>();
                temp.GetComponent<Trash>().isFloatingText = true;
            }

        }

    }

    void OnTriggerExit(Collider other)
    {
        game.inCube = false;
    }
}
