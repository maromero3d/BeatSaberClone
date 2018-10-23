using UnityEngine;
using System.Collections;

/**
 * Move randomly within the given box collider.
 * The collider must be set to "Is Trigger" or else the objects would of course interact with it
 * The positions are used as they come from the collider, without considering the gameobject's bounds. So if you have a sphere, you need to consider its size when you position / set the dimensions of the collider
 */
public class MoveRandomlyInCollider : MonoBehaviour {

    // the speed of the game object
    public float speed = 3f;

    // the box within the random values will be generated
    // IMPORTANT: the collider must be set to "Is Trigger" or else the objects would of course interact with it
    public BoxCollider boxCollider;

    // the distance at which the target is considered "reached"
    private float targetReachedDelta = 0.1f;

    // the current target position
    private Vector3 targetPosition;

    public bool moveEnabled = false;

    /// <summary>
    /// Normally the movement is linear. If you use smooth damp, the movement will be dampened.
    /// See https://docs.unity3d.com/ScriptReference/Vector3.SmoothDamp.html
    /// </summary>
    public bool useSmoothDamp = true;
    public float smoothTime = 0.3F;
    private Vector3 smoothDampVelocity = Vector3.zero;

    /// <summary>
    /// Idle movement: no static position, instead make the object appear as if it moves up and down. Gives the impression of hovering.
    /// </summary>
    public bool idleMovementEnabled = true;
    private bool isIdleMovementUp = true; // internally used hover toggle information
    private float idleMovementMaxSpeed = 0.1f; // maximum speed limitation. for some reason the smooth damp's smooth time doesn't work properly. tried with very samll and very high values; result was the same
    private float idleMovementPositionY = 0.2f; // maximum distance that the object should move in y direction  (up and down)

    void Start () {
        targetPosition = createRandomTargetPosition();
    }
	
	// Update is called once per frame
	void Update () {

        // keyboard handler
        // toggle enabled state with keyboard
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleActivation();
        }

        if ( !moveEnabled && !idleMovementEnabled)
            return;

        if (Vector3.Distance(transform.position, targetPosition) < targetReachedDelta)
        {
            targetPosition = createRandomTargetPosition();
        }

        float step = speed * Time.deltaTime;

        if(useSmoothDamp)
        {
            // maximum movement speed
            float maxSpeed = Mathf.Infinity;
            if (!moveEnabled)
            {
                maxSpeed = idleMovementMaxSpeed;
            }

            // calculate position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothDampVelocity, smoothTime, maxSpeed);

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        }
    }

    public void ToggleActivation()
    {
        moveEnabled = !moveEnabled;
    }

    // create a random position within the box collider
    private Vector3 createRandomTargetPosition()
    {
        if(moveEnabled)
        {
            return GetPointInCollider(boxCollider);
        }

        if(idleMovementEnabled)
        {
            return getIdleMovementPosition();
        }

        // should never occur
        return transform.position;
    }

    /// <summary>
    /// Returns a random world point inside the given BoxCollider
    /// </summary>
    private Vector3 GetPointInCollider(BoxCollider area)
    {
        Vector3 bLocalScale = area.transform.localScale;
        Vector3 boxPosition = area.transform.position;
        boxPosition += new Vector3(bLocalScale.x * area.center.x, bLocalScale.y * area.center.y, bLocalScale.z * area.center.z);

        Vector3 dimensions = new Vector3(bLocalScale.x * area.size.x,
        bLocalScale.y * area.size.y,
        bLocalScale.z * area.size.z);

        Vector3 newPos = new Vector3(UnityEngine.Random.Range(boxPosition.x - (dimensions.x / 2), boxPosition.x + (dimensions.x / 2)),
        UnityEngine.Random.Range(boxPosition.y - (dimensions.y / 2), boxPosition.y + (dimensions.y / 2)),
        UnityEngine.Random.Range(boxPosition.z - (dimensions.z / 2), boxPosition.z + (dimensions.z / 2)));

        return newPos;
    }

    private Vector3 getIdleMovementPosition()
    {
        Vector3 newPos;

        if (isIdleMovementUp)
        {
            newPos = new Vector3(transform.position.x, transform.position.y + idleMovementPositionY, transform.position.z);
        }
        else
        {
            newPos = new Vector3(transform.position.x, transform.position.y - idleMovementPositionY, transform.position.z);
        }

        isIdleMovementUp = !isIdleMovementUp;

        return newPos;
    }
}
