//if accuracy is 0.001 = good performance | if 0.000001 laggy performance
    public Vector3[] GetPoints (float gap,float accuracy){
     SimpsonVec sv = SV_Setup(0);
     Vector3 last_spawn = Bezier.GetPoint(sv.A,sv.B,sv.C,sv.D,0);

     List<Vector3> allPoints = new List<Vector3>();
     allPoints.Add(last_spawn);

     for(float t = accuracy;t <= 1.0f; t +=accuracy){
         Vector3 trial = Bezier.GetPoint(sv.A,sv.B,sv.C,sv.D,t);
         if(Vector3.Distance(trial,last_spawn) >= gap){
             last_spawn = trial;
             allPoints.Add(trial);
         }
     }
     return allPoints.ToArray();
 }
 public Vector3[] GetAllPoints(float gap,float acc){

    SimpsonVector = SV_SETUP_ALL();
    BezierPoints bp = new BezierPoints();
    bp.bp_vector3 = new List<Vector3>();
    bp.bp_lastSpawn = new List<Vector3>();

    for(int i = 0; i<points.Length / 3;i++){

        Vector3 ls = new Vector3();
        if(i == 0){
            ls = Bezier.GetPoint(SimpsonVector[0].A,SimpsonVector[0].B,SimpsonVector[0].C,SimpsonVector[0].D,0);
        }if (i > 0){
            ls = bp.bp_lastSpawn[i-1];
        }
        BezierPoints bp_temp = GetSegmentPoints(gap,acc,i,ls);
        bp.bp_lastSpawn.Add(bp_temp.bp_lastSpawn[0]);
        bp.bp_vector3.AddRange(bp_temp.bp_vector3);
        SimpsonVector_TEMP = SimpsonVector;
    }


    return bp.bp_vector3.ToArray();
}

BezierPoints GetSegmentPoints (float gap,float acc,int index, Vector3 ls)
{
    SimpsonVec sv = SimpsonVector[index];
    Vector3 last_spawn = ls;

    BezierPoints bp = new BezierPoints();
    bp.bp_vector3 = new List<Vector3>();
    bp.bp_lastSpawn = new List<Vector3>();

    float step = 0.1f;
    float t = step;
    float lastT = new float();

    while (t >= 0 && t <= 1f)
    {
        while (t < 1f && Vector3.Distance(Bezier.GetPoint(sv.A,sv.B,sv.C,sv.D,t), last_spawn) < gap){
            t += step;}
        step /= acc;
        while (t > lastT && Vector3.Distance(Bezier.GetPoint(sv.A,sv.B,sv.C,sv.D,t), last_spawn) > gap){
            t -= step;}
        step /= acc;
        if (t > 1f || t < lastT){
            break;}
        if(step < 0.000001f){
            last_spawn = Bezier.GetPoint(sv.A,sv.B,sv.C,sv.D,t);
            bp.bp_vector3.Add(last_spawn + transform.position);
            lastT = t;
            step = 0.1f;
        }
    }
    bp.bp_lastSpawn.Add(last_spawn);
    return bp;
}