using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour {

    public GameObject head;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(head.transform.position.x, head.transform.position.y, head.transform.position.z - 9f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Box")
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
}
