void Start()
{
    // get a new gesture recognizer
    recognizer = new GestureRecognizer();
    // set up to receive both tap and double tap events
    recognizer.SetRecognizableGestures(GestureSettings.Tap | GestureSettings.DoubleTap);
    // see https://docs.unity3d.com/550/Documentation/ScriptReference/VR.WSA.Input.GestureRecognizer.TappedEventDelegate.html 
    recognizer.TappedEvent += (source, tapCount, ray) =>
    {
        if (tapCount == 1)
        {
            Debug.Log("Tap");
        }
        else if (tapCount == 2)
        {
            Debug.Log("Double Tap");
        }
    };
    recognizer.StartCapturingGestures();
}

const float DELAY = .5f;

void Start()
{
    recognizer = new GestureRecognizer();
    recognizer.StartCapturingGestures();

    recognizer.SetRecognizableGestures(GestureSettings.Tap | GestureSettings.DoubleTap);
    recognizer.TappedEvent += (source, tapCount, ray) =>
    {
        if (tapCount == 1)
            Invoke("SingleTap", DELAY);
        else if (tapCount == 2)
        {
            CancelInvoke("SingleTap");
            DoubleTap();
        }
    };
}

void SingleTap()
{
    Debug.Log("Single Tap")
}

void DoubleTap()
{
    Debug.Log("Double Tap")
}