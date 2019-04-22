public class RotationController : MonoBehaviour {
    Quaternion rotationAtStart;
    int numOfRotations = 0;

    void rotate () {
        numOfRotations++;
        if (numOfRotations == 1) {
            rotationAtStart = transform.rotation;
        } else if (numOfRotations < 9) {
            transform.Rotate (new Vector3 (0,40.0f,0));
        } else if (numOfRotations == 9) {
            transform.rotation = rotationAtStart;
        }
    }
    void Update () {
        if (numOfRotations < 9) {
            rotate ();
        }
    }
 }   
 using UnityEngine;
using System.Collections;

public class ExtensionVector3 : MonoBehaviour {

    public static float CalculateEulerSafeX(float x){
        if( x > -90 && x <= 90 ){
            return x;
        }

        if( x > 0 ){
            x -= 180;
        } else {
            x += 180;
        }
        return x;
    }

    public static Vector3 EulerSafeX(Vector3 eulerAngles){
        eulerAngles.x = CalculateEulerSafeX(eulerAngles.x);
        return eulerAngles;
    }
}