void Start () 
{
    StartCoroutine (StableWaittingTime ());
}

IEnumerator StableWaittingTime ()
{
    yield return new WaitForSeconds (1f);
    if (false) 
    {
        // do something
    } 
    else 
    {
        // do something
        StartCoroutine (StableWaittingTime ());
        yield break;
    }
}