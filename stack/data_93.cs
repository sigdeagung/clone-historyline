public RectTransform targetRC;
UnityEngine.Object driver;

void Start()
{
    DrivenRectTransformTracker dt = new DrivenRectTransformTracker();
    dt.Clear();

    //Object to drive the transform
    driver = this;
    dt.Add(driver, targetRC, DrivenTransformProperties.All);
}