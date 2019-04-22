public class CustomTrackableEventHandler : MonoBehaviour,
                                           ITrackableEventHandler
{
    ...

    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound(); 
            // **** Your own logic here ****
        }
        else
        {
            OnTrackingLost();
        }
    }
}
YourMarker(ImageTarget)
     |__EmptyPlaceHolder

     var placeHolder = GameObject.Find("EmptyPlaceHolder");
if(placeHolder != null){
    Debug.Log(placeHolder.transform.position); // all the location, localPosition, quaternion etc will be available to you

}