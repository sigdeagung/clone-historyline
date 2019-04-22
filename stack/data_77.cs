public class MonoScript : MonoBehaviour
{
    void Start()
    {
        NonMonoScript  nonMonoScript = new NonMonoScript();
        //Pass MonoBehaviour to non MonoBehaviour class
        nonMonoScript.monoParser(this);
    }
}
public class NonMonoScript 
{
    public void monoParser(MonoBehaviour mono)
    {
        //We can now use StartCoroutine from MonoBehaviour in a non MonoBehaviour script
        mono.StartCoroutine(testFunction());

       //And also use StopCoroutine function
        mono.StopCoroutine(testFunction());
    }

    IEnumerator testFunction()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Test!");
    }
}