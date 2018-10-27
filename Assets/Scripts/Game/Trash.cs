using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour {

    public bool isFloatingText = false;
    public float _time = 0.3f;
    // Use this for initialization
    void Start()
    {
        Invoke("delete", _time);
    }

    void delete()
    {
        //if(!isFloatingText)
            Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if(isFloatingText)
            gameObject.GetComponent<Transform>().Translate(3f * Vector3.up * Time.deltaTime, Space.World);
    }
}
