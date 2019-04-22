float rot_duration = 10f;
float rot_speed = 3f;
Quaternion final_rot;
GameObject cubeMesh;

void Start()
{
    cubeMesh = GameObject.FindWithTag("CubeMesh");

    Vector3 initial_rot = transform.rotation.eulerAngles;
    final_rot = Quaternion.Euler(new Vector3(initial_rot.x, initial_rot.y, 180));
}

public void Update()
{
    if (Input.GetMouseButtonUp(0))
    {
        StartCoroutine(Delay(1));
    }
}

IEnumerator Delay(float waitTime)
{
    yield return new WaitForSeconds(waitTime);
    float rot_elapsedTime = 0.0F;

    //Get the current rotation 
    Quaternion currentLocation = cubeMesh.transform.rotation;

    while (rot_elapsedTime < rot_duration)
    {
        rot_elapsedTime += Time.deltaTime;
        cubeMesh.transform.rotation = Quaternion.Slerp(currentLocation, final_rot, rot_elapsedTime / rot_duration);
        yield return null;
    }
}
float rot_duration = 10f;
float rot_speed = 3f;
float rot_elapsedTime = 3f;
Quaternion final_rot;

public void Update()
{
    if (Input.GetMouseButtonUp(0)) 
    {
        StartCoroutine (Delay (1));
    }
    if (rot_elapsedTime < rot_duration) {
        cubeMesh.transform.rotation = Quaternion.Slerp (transform.rotation, final_rot, rot_elapsedTime);
        rot_elapsedTime += Time.deltaTime * rot_speed;
}

IEnumerator Delay(float waitTime)
{
   yield return new WaitForSeconds (waitTime);
   rot_elapsedTime = 0.0F;
}