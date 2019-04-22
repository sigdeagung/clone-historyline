private Vector3 ballPosition;
public bool spawnNewBall;

void Start() {
    spawnNewBall = true;
}

void OnTriggerEnter2D (Collider2D other) {

    if (other.gameObject.tag == "Ball") {

        if (spawnNewBall) {
            other.GetComponent</*YourScriptName*/>().spawnNewBall = false;
        }

        ballPosition = new Vector3 ((transform.position.x + other.transform.position.x) / 2, (transform.position.y + other.transform.position.y) / 2, 0.0f);
        StartCoroutine ("RespawnBall");
    }
}

IEnumerator RespawnBall () {
    if (spawnNewBall) {
        Instantiate (gameObject, ballPosition, Quaternion.identity);
    }
    Destroy (gameObject);
    yield return null;
}

public class TwoToOne : MonoBehaviour {

    public bool doNothing;

    void OnCollisionEnter (Collision col)
        {
        if (doNothing) return;

        col.gameObject.GetComponent<TwoToOne>().doNothing = true;
        Destroy(col.gameObject);

        GameObject newCube = Instantiate(gameObject);

        Destroy(gameObject);
        }

    }


    using UnityEngine;
using System.Collections;

public static class EventManager{

    // Create our delegate with expected params.
    // NOTE params must match SphereScript.PostCollision declaration
    public delegate void CollideEvent(string message);

    // Create the delegate instance. This is the one we will invoke.
    public static event CollideEvent PostCollision;

    // Called whenever an object has collided with another
    public static void Collision(GameObject obj1, GameObject obj2, Vector3 collisionPoint){
        if (obj1.GetComponent<sphereScript>().isAlive && obj2.GetComponent<sphereScript>().isAlive) {

            //Kill the 2 objects which haev collided.
            obj1.GetComponent<sphereScript> ().Kill ();
            obj2.GetComponent<sphereScript> ().Kill ();

            //Create a cube.
            GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
            cube.transform.position = collisionPoint;

            // Invoke delegate invocation list
            PostCollision("Something is dead");
        }
    }
}

using UnityEngine;
using System.Collections;

public class sphereScript : MonoBehaviour {
    // Am I alive?
    public bool isAlive;

    // Use this for initialization
    void Start () {
        // Add a function we want to be called when the EventManager invokes PostCollision
        EventManager.PostCollision += PostCollision;
        isAlive = true;
    }

    // Update is called once per frame
    void Update () {

    }

    //Invoked from EventManager.PostCollision delegate
    void PostCollision(string message){
        if(isAlive)
            Debug.Log (this.name + " message received: " + message);
    }

    // Called when it is time to destroy this gameobject
    public void Kill(){
        isAlive = false;
        Destroy (this.gameObject);
    }

    //On collision with another object
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.GetComponent<sphereScript>()) {
            EventManager.Collision (this.gameObject, collision.gameObject, collision.contacts [0].point);
        }
    }

    // Called after this object has been destroyed
    void OnDestroy(){
        // cleanup events for performance.
        EventManager.PostCollision -= PostCollision;
    }
}