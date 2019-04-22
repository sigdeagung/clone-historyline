public void AttachToDoor()
 {
 private bool isOpen;
 public float doorSpeed;
 etc etc
 public void OpenCloseDoor()
   {
   your code to open/close a door
   }
 }
 public void AttachToButton()
{
public AttachToDoor amazaingDoorScript;
etc etc
void Update()
 {
  if (Input.GetButton("Fire1"))
   if (DoPlayerLookAtButton() && isAnimationReadyToPlay)
    amazaingDoorScript.OpenCloseDoor();
 }
}
 public AttachToDoor amazaingDoorScript; ...
   Invoke("test",Random.Range(5f,10f)); ...
   private void test()
     {
     // have a ghost mess with the door occasionally
     amazaingDoorScript.OpenCloseDoor();
     Invoke("test",Random.Range(5f,10f));
     }

     public void AttachToButton()
{
public UnityEvent buttonClicked;
 void Update()
 {
  if (Input.GetButton("Fire1"))
   if (DoPlayerLookAtButton() && isAimationReadyToPlay)
    {
    Debug.Log("Button pressed!");
    if (buttonClicked!=null) buttonClicked.Invoke();
    }
 }
}