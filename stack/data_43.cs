AndroidJavaClass unityClass;
AndroidJavaObject unityActivity;
AndroidJavaClass customClass;

void Start()
{
    //Replace with your full package name
    sendActivityReference("com.example.StatusCheckStarter");

   //Now, start service
   startService();
}

void sendActivityReference(string packageName)
{
    unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
    customClass = new AndroidJavaClass(packageName);
    customClass.CallStatic("receiveActivityInstance", unityActivity);
}

void startService()
{
    customClass.CallStatic("StartCheckerService");
}
void Start()
{
    //Replace with your full package name
    startService("com.example.StatusCheckStarter");
}

void startService(string packageName)
{
    AndroidJavaClass unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    AndroidJavaObject unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
    AndroidJavaClass customClass = new AndroidJavaClass(packageName);
    customClass.CallStatic("StartCheckerService", unityActivity);
}