using UnityEngine;
using System.Collections;

public class test: MonoBehaviour 
{
    public int playerScore;

    void Start()
    {
        anotherScript = gameObject.GetComponent<AnotherScript>();
    }

    void Update ()
    {
       Debug.Log("The player's score is " + anotherScript.playerScore);
    }
}
public GameObject otherGameObject;


private AnotherScript anotherScript;


void Awake ()
{
    anotherScript = otherGameObject.GetComponent<AnotherScript>();

}

void Update ()
{
    Debug.Log("The player's score is " + anotherScript.playerScore);
}

}