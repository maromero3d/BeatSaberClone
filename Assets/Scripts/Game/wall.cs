using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour {

    public GameObject head;
    public GameObject controllerLeft;
    public GameObject controllerRight;
    public GameObject L_HAND;
    public GameObject R_HAND;
    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(head.transform.position.x, head.transform.position.y, head.transform.position.z - 9f);

        /*var jointL = AddFixedJoint(controllerLeft);
        jointL.connectedBody = L_HAND.GetComponent<Rigidbody>();
        var jointR = AddFixedJoint(controllerRight);
        jointR.connectedBody = R_HAND.GetComponent<Rigidbody>();*/
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

    private FixedJoint AddFixedJoint(GameObject obj)
    {
        FixedJoint fx = obj.AddComponent<FixedJoint>();
        fx.breakForce = Mathf.Infinity;
        fx.breakTorque = Mathf.Infinity;
        return fx;
    }
}
