void OnMouseOver()
{
    if (Input.GetMouseButtonDown(1)) //you get passed that if when you hit first time
    {
        HitCounter++;
        StartCoroutine(ShowHitCounter(HitCounter.ToString(), 2)); //you call your label with delay of 2 sec
    }
}

IEnumerator ShowHitCounter(string message, float delay)
{
    GUIHit.text = message;
    GUIHit.enabled = true;
    yield return new WaitForSeconds(delay); // still on your first hit you get to here and wait 2 seconds
    HitCounter = 0; //after 2 seconds you reset hitcounter and disable label
    GUIHit.enabled = false;
}

IEnumerator ShowHitCounter(string message)
{
    GUIHit.text = message;
    GUIHit.enabled = true;
}
void ClearLabel()
{
    HitCounter = 0; 
    GUIHit.enabled = false;
}

public static DateTime TimeLeft { get; set; }

void OnMouseOver()
{
    TimeSpan span = DateTime.Now - TimeLeft;
    int ms = (int)span.TotalMilliseconds;
    if (ms > 2000)
    {
        ClearLabel();
    }
    if (Input.GetMouseButtonDown(1)) 
    {
        HitCounter++;
        StartCoroutine(ShowHitCounter(HitCounter.ToString(), 2)); 
    }
}

public Text GUIHit;
public int HitCounter = 0;
bool firstRun = true;

float waitTimeBeforeDisabling = 2f;
float timer = 0;

void Update()
{
    //Check when Button is Pressed
    if (Input.GetMouseButtonDown(1))
    {
        //Reset Timer each time  there is a right click
        timer = 0;

        if (!firstRun)
        {
            firstRun = true;
            GUIHit.enabled = true;
        }

        HitCounter++;
        GUIHit.text = HitCounter.ToString();
    }

    //Button is not pressed
    else
    {
        //Increement timer if Button is not pressed and timer < waitTimeBeforeDisabling
        if (timer < waitTimeBeforeDisabling)
        {
            timer += Time.deltaTime;
        }

        //Timer has reached value to Disable Text
        else
        {
            if (firstRun)
            {
                firstRun = false;
                GUIHit.text = HitCounter.ToString();
                HitCounter = 0;
                GUIHit.enabled = false;
            }
        }

    }
}