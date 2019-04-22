private float _RawLerp;
private float _Lerp;
public float _Speed;
public transform _Source;
public transform _Target;

private transform _TransformCache; // the transform for my game object, set in the Awake method

public void Update()
{
    _RawLerp += Time.deltaTime * _Speed;
     _Lerp = Mathf.Min(_RawLerp, 1); 
   _TransformCache.rotation = Quaternion.Slerp(
         _Source.TargetRotation(),
         _Target.TargetRotation(), 
         _Lerp);
}

Plane plane = new Plane(Vector3.up, 0);
float dist;
void Update () {
    //cast ray from camera to plane (plane is at ground level, but infinite in space)
    Ray ray = Camera.mainCamera.ScreenPointToRay(Input.mousePosition);
    if (plane.Raycast(ray, out dist)) {
        Vector3 point = ray.GetPoint(dist);

        //find the vector pointing from our position to the target
        Vector3 direction = (point - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        float angle = Quaternion.Angle(transform.rotation, lookRotation);
        float timeToComplete = angle / rotationSpeed;
        float donePercentage = Mathf.Min(1F, Time.deltaTime / timeToComplete);

        //rotate towards a direction, but not immediately (rotate a little every frame)
        //The 3rd parameter is a number between 0 and 1, where 0 is the start rotation and 1 is the end rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, donePercentage);
    }
}