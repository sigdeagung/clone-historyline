public GameObject objectectA;
public GameObject objectectB;

void Start()
{
    StartCoroutine(moveToX(objectectA.transform, objectectB.transform.position, 1.0f));
}


bool isMoving = false;

IEnumerator moveToX(Transform fromPosition, Vector3 toPosition, float duration)
{
    //Make sure there is only one instance of this function running
    if (isMoving)
    {
        yield break; ///exit if this is still running
    }
    isMoving = true;

    float counter = 0;

    //Get the current position of the object to be moved
    Vector3 startPos = fromPosition.position;

    while (counter < duration)
    {
        counter += Time.deltaTime;
        fromPosition.position = Vector3.Lerp(startPos, toPosition, counter / duration);
        yield return null;
    }

    isMoving = false;
}
float duration; //duration of movement
float durationTime; //this will be the value used to check if Time.time passed the current duration set

void Start()
{
    StartMovement();
}

void StartMovement()
{
    InvokeRepeating("MovementFunction", Time.deltaTime, Time.deltaTime); //Time.deltaTime is the time passed between two frames
    durationTime = Time.time + duration; //This is how long the invoke will repeat
}

void MovementFunction()
{
    if(durationTime > Time.time)
    {
        //Movement
    } 
    else 
    {
        CancelInvoke("MovementFunction"); //Stop the invoking of this function
        return;
    }
}

private IEnumerator foo()
{
    while(yourCondition) //for example check if two seconds has passed
    {
        //move the player on a per frame basis.
        yeild return null;
    }
}