void functionCalledVeryOften()
{
    float[] playerLives = new float[5]; //This is bad because it allocates memory each time it is called
    for (int i = 0; i < playerLives.Length; i++)
    {
        playerLives[i] = UnityEngine.Random.Range(0f,5f);
    }
}
float[] playerLives = new float[5]; 
void functionCalledVeryOften()
{
    for (int i = 0; i < playerLives.Length; i++)
    {
        playerLives[i] = UnityEngine.Random.Range(0f,5f);
    }
}
void functionCalledVeryOften()
{
    if (Input.GetKey(KeyCode.Space))
    {
        //Create new Bullet each time
        GameObject myObject = new GameObject("bullet");
        Rigidbody bullet = myObject.AddComponent<Rigidbody>() as Rigidbody;
        //Shoot Bullet
        bullet.velocity = transform.forward * 50;
        Destroy(myObject);
    }
}
List<GameObject> reUsableBullets;
int toUseIndex = 0;

void Start()
{
    intitOnce();
}

//Call this function once to create bullets
void intitOnce()
{
    reUsableBullets = new List<GameObject>();

    //Create 20 bullets then store the reference to a global variable for re-usal
    for (int i = 0; i < 20; i++)
    {
        reUsableBullets[i] = new GameObject("bullet");
        reUsableBullets[i].AddComponent<Rigidbody>();
        reUsableBullets[i].SetActive(false);
    }
}

void functionCalledVeryOften()
{
    if (Input.GetKey(KeyCode.Space))
    {
        //Re-use old bullet
        reUsableBullets[toUseIndex].SetActive(true); 
        Rigidbody tempRgb = reUsableBullets[toUseIndex].GetComponent<Rigidbody>();

        tempRgb.velocity = transform.forward * 50;
        toUseIndex++;

        //reset counter
        if (toUseIndex == reUsableBullets.Count - 1)
        {
            toUseIndex = 0;
        }
    }
}
public GameObject bulletPrefab;
void functionCalledVeryOften()
{
    if (Input.GetKey(KeyCode.Space))
    {
        //Create new Bullet each time
        Rigidbody bullet = Instantiate(bulletPrefab, new Vector3(0, 0, 0), Quaternion.identity) as Rigidbody; 
        //Shoot Bullet
        bullet.velocity = transform.forward * 50;
        Destroy(myObject,10f);
    }
}