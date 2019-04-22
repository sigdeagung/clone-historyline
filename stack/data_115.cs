public IEnumerator HowToSmoothly()
    {
    // move "X" from value "A" to value "B"

    float duration = 2.5f;
    float delta = B - A;

    float startTime = Time.time;
    float finishTime = Time.time+duration;

    while(Time.time<finishTime)
        {

        float soFarTime = Time.time-startTime;
        float fractionThisFrame = soFarTime / duration;
        float valueThisFrame = A + delta * fractionThisFrame;

        X = valueThisFrame
        if (X > B) X = B;

        yield return 0;
        }

    X = B;
    yield break;
    }

    public class PointRotator : MonoBehaviour
    {
    bool rotating = false;
float rate = 0;
float angle;

Vector3 point;
Vector3 pivot;

public void Rotate(Vector3 point, Vector3 pivot, float duration, float angle)
{
    this.point = point;
    this.pivot = pivot;
    this.rate = angle/duration;
    this.angle = angle;

    rotating = true;
}

public void Update()
{
    if (rotating)
    {
        // use quartonian.Lerp with Time.deltatime here

    //if(angle > quartonian angle){rotating = false)
    }
}
    }