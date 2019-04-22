void Update () {
    // handle ball rotation 
    float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
    rotation *= Time.deltaTime;
    rigidbody.AddRelativeTorque(Vector3.back * rotation);  

    //check input
    if (Input.GetKeyDown(KeyCode.W)) 
    {
        rigidbody.AddForce(new Vector3(0, jumpHeight, 0) * Time.deltaTime); 
    }
}

public int rotationSpeed = 100;
public float jumpHeight = 8;

private bool isFalling = false;

void Update () {
    // handle ball rotation 
    float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
    rotation *= Time.deltaTime;
    rigidbody.AddRelativeTorque(Vector3.back * rotation);  

    //check input
    if (Input.GetKeyDown(KeyCode.W)) 
    {
        rigidbody.velocity = new Vector3(0, jumpHeight, 0); 
    }
}