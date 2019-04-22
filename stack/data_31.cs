private List<float> lengths = new List<float>();
private List<float> speeds = new List<float>();
private List<float> accels = new List<float>();
public float spdInit = 0;

private void Start()
{
  wayPoints.Sort((x, y) => x.time.CompareTo(y.time));
  wayPoints.Insert(0, wayPoints[0]);
  wayPoints.Add(wayPoints[wayPoints.Count - 1]);
       for (int seg = 1; seg < wayPoints.Count - 2; seg++)
  {
    Vector3 p0 = wayPoints[seg - 1].pos;
    Vector3 p1 = wayPoints[seg].pos;
    Vector3 p2 = wayPoints[seg + 1].pos;
    Vector3 p3 = wayPoints[seg + 2].pos;
    float len = 0.0f;
    Vector3 prevPos = GetCatmullRomPosition(0.0f, p0, p1, p2, p3, catmullRomAlpha);
    for (int i = 1; i <= Mathf.FloorToInt(1f / debugTrackResolution); i++)
    {
      Vector3 pos = GetCatmullRomPosition(i * debugTrackResolution, p0, p1, p2, p3, catmullRomAlpha);
      len += Vector3.Distance(pos, prevPos);
      prevPos = pos;
    }
    float spd0 = seg == 1 ? spdInit : speeds[seg - 2];
    float lapse = wayPoints[seg + 1].time - wayPoints[seg].time;
    float acc = (len - spd0 * lapse) * 2 / lapse / lapse;
    float speed = spd0 + acc * lapse;
    lengths.Add(len);
    speeds.Add(speed);
    accels.Add(acc);
  }
}

public Vector3 GetPosition(float time)
{
  //Check if before first waypoint
  if (time <= wayPoints[0].time)
  {
    return wayPoints[0].pos;
  }
  //Check if after last waypoint
  else if (time >= wayPoints[wayPoints.Count - 1].time)
  {
    return wayPoints[wayPoints.Count - 1].pos;
  }

  //Check time boundaries - Find the nearest WayPoint your object has passed
  float minTime = -1;
  // float maxTime = -1;
  int minIndex = -1;
  for (int i = 1; i < wayPoints.Count; i++)
  {
    if (time > wayPoints[i - 1].time && time <= wayPoints[i].time)
    {
      // maxTime = wayPoints[i].time;
      int index = i - 1;
      minTime = wayPoints[index].time;
      minIndex = index;
    }
  }

  float spd0 = minIndex == 1 ? spdInit : speeds[minIndex - 2];
  float len = lengths[minIndex - 1];
  float acc = accels[minIndex - 1];
  float t = time - minTime;
  float posThroughSegment = spd0 * t + acc * t * t / 2;
  float percentageThroughSegment = posThroughSegment / len;

  //Define the 4 points required to make a Catmull-Rom spline
  Vector3 p0 = wayPoints[ClampListPos(minIndex - 1)].pos;
  Vector3 p1 = wayPoints[minIndex].pos;
  Vector3 p2 = wayPoints[ClampListPos(minIndex + 1)].pos;
  Vector3 p3 = wayPoints[ClampListPos(minIndex + 2)].pos;

  return GetCatmullRomPosition(percentageThroughSegment, p0, p1, p2, p3, catmullRomAlpha);
}