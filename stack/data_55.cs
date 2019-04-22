public class BCurve : MonoBehaviour {

    LineRenderer lineRenderer;
    public Vector3 point0, point1, point2;
    int numPoints = 50;
    Vector3[] positions = new Vector3[50];

    // Use this for initialization
    void Start () {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material (Shader.Find ("Sprites/Default"));
        lineRenderer.startColor = lineRenderer.endColor = Color.white;
        lineRenderer.startWidth = lineRenderer.endWidth = 0.1f;

        StartCoroutine(DrawQuadraticCurve (3));

    }

    IEnumerator DrawQuadraticCurve (float duration)  {
        //Calculate wait duration for each loop so it match 3 seconds
        float waitDur = duration / numPoints;

        for (int i = 1; i < numPoints + 1; i++) {
            float t = i / (float)numPoints;
            lineRenderer.positionCount = i;
            lineRenderer.setPosition(i - 1, CalculateLinearBeziearPoint (t, point0, point1, point2));
            yield return new WaitForSeconds(waitDur);
        }
    }

    Vector3 CalculateLinearBeziearPoint (float t, Vector3 p0, Vector3 p1, Vector3 p2)   {

        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0 + 2 * u * t * p1 + tt * p2;

        return p;
    }

}

 IEnumerator DrawQuadraticCurve (float duration)  {
        float progressPerSecond = 1 / duration;
        float startTime = Time.time;
        float progress = 0;
        while (progress < 1) {
            progress = Mathf.clamp01((Time.time - startTime) * progressPerSecond);
            int prevPointCount = lineRenderer.positionCount;
            int curPointCount = progress * numPoints;
            lineRenderer.positionCount = curPointCount;
            for (int i = prevPointCount; i < curPointCount; ++i) {
                float t = i / (float)numPoints;
                lineRenderer.setPosition(i, CalculateLinearBeziearPoint (t, point0, point1, point2));
            }
            yield return null;
        }
    }

    public class BCurve : MonoBehaviour {

    LineRenderer lineRenderer;
    public Vector3 point0, point1, point2;
    int numPoints = 50;
    Vector3[] positions = new Vector3[50];

    // Use this for initialization
    void Start () {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material (Shader.Find ("Sprites/Default"));
        lineRenderer.startColor = lineRenderer.endColor = Color.white;
        lineRenderer.startWidth = lineRenderer.endWidth = 0.1f;

        StartCoroutine(DrawQuadraticCurve (3));
    }

    IEnumerator DrawQuadraticCurve (float duration)  {
        //Calculate wait duration for each loop so it match 3 seconds
        float waitDur = duration / numPoints;

        for (int i = 1; i < numPoints + 1; i++) {
            float t = i / (float)numPoints;
            lineRenderer.positionCount = i;
            lineRenderer.setPosition(i - 1, CalculateLinearBeziearPoint (t, point0, point1, point2));
            yield return new WaitForSeconds(waitDur);
        }
    }

    Vector3 CalculateLinearBeziearPoint (float t, Vector3 p0, Vector3 p1, Vector3 p2)   {

        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0 + 2 * u * t * p1 + tt * p2;

        return p;
    }

}