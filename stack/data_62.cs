[StructLayout(LayoutKind.Explicit)]
public class OverlapEvents
{
    [FieldOffset(0)]
    public VRInput Source;

    [FieldOffset(0)]
    public EventCapture Target;
}
public class EventCapture
{
    public event Action OnClick;

    public void SimulateClick()
    {
        InvokeClicked();
    }

    // This method will call the event from VRInput!
    private void InvokeClicked()
    {
        var handler = OnClick;
        if (handler != null)
            handler();
    }
}
public static void Main()
{
    input = GetComponent<VRInput>();

    // Overlap the event
    var o = new OverlapEvents { Source = input };

    // You can now call the event! (Note how Target should be null but is of type VRInput)
    o.Target.SimulateClick();
}