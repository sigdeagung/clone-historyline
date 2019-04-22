using UnityEngine;
using System.Collections;

public class Follow: MonoBehaviour {
public Transform target;
public float smooth= 5.0f;
void  Update (){
    transform.position = Vector3.Lerp (
        transform.position, target.position,
        Time.deltaTime * smooth);
} 

    } 

    using UnityEngine;
using System.Collections;

public class SmoothFollowScript: MonoBehaviour {

// The target we are following
public  Transform target;
// The distance in the x-z plane to the target
public int distance = 10.0;
// the height we want the camera to be above the target
public int height = 10.0;
// How much we 
public heightDamping = 2.0;
public rotationDamping = 0.6;


void  LateUpdate (){
    // Early out if we don't have a target
    if (TargetScript.russ == true){
    if (!target)
        return;

    // Calculate the current rotation angles
    wantedRotationAngle = target.eulerAngles.y;
    wantedHeight = target.position.y + height;

    currentRotationAngle = transform.eulerAngles.y;
    currentHeight = transform.position.y;

    // Damp the rotation around the y-axis
    currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

    // Damp the height
    currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);

    // Convert the angle into a rotation
    currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);

    // Set the position of the camera on the x-z plane to:
    // distance meters behind the target
    transform.position = target.position;
    transform.position -= currentRotation * Vector3.forward * distance;

    // Set the height of the camera
    transform.position.y = currentHeight;

    // Always look at the target
    transform.LookAt (target);
}
}
}
using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

public float interpVelocity;
public float minDistance;
public float followDistance;
public GameObject target;
public Vector3 offset;
Vector3 targetPos;
// Use this for initialization
void Start () {
    targetPos = transform.position;
}

// Update is called once per frame
void FixedUpdate () {
    if (target)
    {
        Vector3 posNoZ = transform.position;
        posNoZ.z = target.transform.position.z;

        Vector3 targetDirection = (target.transform.position - posNoZ);

        interpVelocity = targetDirection.magnitude * 5f;

        targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime); 

        transform.position = Vector3.Lerp( transform.position, targetPos + offset, 0.25f);

    }
   }
}
using UnityEngine;
using System.Collections;

        public class SmoothFollow2 : MonoBehaviour {
        public Transform target;
        public float distance = 3.0f;
        public float height = 3.0f;
        public float damping = 5.0f;
        public bool smoothRotation = true;
        public bool followBehind = true;
        public float rotationDamping = 10.0f;

        void Update () {
               Vector3 wantedPosition;
               if(followBehind)
                       wantedPosition = target.TransformPoint(0, height, -distance);
               else
                       wantedPosition = target.TransformPoint(0, height, distance);

               transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * damping);

               if (smoothRotation) {
                       Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
                       transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
               }
               else transform.LookAt (target, target.up);
         }
}