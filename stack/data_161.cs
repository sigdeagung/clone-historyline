IEnumerator Example() 
{
    print(Time.time);
    yield return new WaitForSeconds(5);
    print(Time.time);
}
void Start() 
{
    print("Starting " + Time.time);
    StartCoroutine(WaitAndPrint(2.0F));
    print("Before WaitAndPrint Finishes " + Time.time);
}

IEnumerator WaitAndPrint(float waitTime) 
{
    yield return new WaitForSeconds(waitTime);
    print("WaitAndPrint " + Time.time);
}
using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour {
    IEnumerator Example() {
        print(Time.time);
        yield return new WaitForSeconds(5);
        print(Time.time);
    }
}