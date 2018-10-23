using UnityEngine;
using System.Collections;

/**
 *  Spin an object
 *  Add script to an object and set x/y/z coordinates to spin value (eg x=100)
 */
public class Spin : MonoBehaviour {

    [SerializeField]
    private Vector3 spinXYZ;

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(spinXYZ * Time.deltaTime);
    }
}
