public float speed = 1.19f;

void Update()
{
    //PingPong between 0 and 1
    float time = Mathf.PingPong(Time.time * speed, 1);
    Debug.Log(time);
}
float timer = 0;
float cycle = 0;
public float speed = 1;

void Update()
{
    timer += Time.deltaTime;
    Cycle();
}

void Cycle()
{
    cycle = (Mathf.Sin(timer) + 1) * 0.5f;
}
float timer = -1;

void Update()
{
  timer += Time.deltaTime;

  if(timer >= 1)
  {
    timer = -1;
  }
    Cycle();
}

void Cycle()
{
    //Do Your Cycle
//-1 is left night, 0 is middle day, 1 is right night
}