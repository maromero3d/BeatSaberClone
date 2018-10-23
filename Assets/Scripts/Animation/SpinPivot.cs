using UnityEngine;
using System.Collections;

/**
 *  Spin an object around a pivot point.
 *  
 *  Add script to an object and set x/y/z coordinates to 0/1/-1 depending on the direction.
 *  Spin speed is specified via speed value (eg 100)
 */
public class SpinPivot : MonoBehaviour {

    [SerializeField]
    private Vector3 spinXYZ;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform pivot;

    void Start () {
	}

    void Update()
    {
        transform.RotateAround(pivot.transform.position, spinXYZ, speed * Time.deltaTime);
    }
}
