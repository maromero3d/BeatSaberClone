using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliceFollow : MonoBehaviour {

    public GameObject trail;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (TwelveNoteGame.playtrack)
        {
            var lookPos = trail.transform.position - transform.position;
            lookPos.z = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1150f);
        }
    }
}
