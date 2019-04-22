IEnumerator waitFunction1()
{
    Debug.Log("Hello Before Waiting");
    yield return new WaitForSeconds(3); //Will wait for 3 seconds then run the code below
    Debug.Log("Hello After waiting for 3 seconds");
}
IEnumerator waitFunction2()
{
    const float waitTime = 3f;
    float counter = 0f;

    Debug.Log("Hello Before Waiting");
    while (counter < waitTime)
    {
        Debug.Log("Current WaitTime: " + counter);
        counter += Time.deltaTime;
        yield return null; //Don't freeze Unity
    }
    Debug.Log("Hello After waiting for 3 seconds");
}