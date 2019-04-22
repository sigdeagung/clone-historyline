using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;

namespace Anything
{
    public class Program
    {
        public static void Main(string[] _)
        {
            var sw = Stopwatch.StartNew();

            Keys()
                .ToObservable()
                .Select(x => new
                {
                    Value = x,
                    Seconds = sw.Elapsed.TotalSeconds
                })
                .Buffer(5, 1)
                .Where(xs => xs.Last().Seconds - xs.First().Seconds <= 1.0)
                .Subscribe(ks => Console.WriteLine($"More than five! {ks.Count}"));
        }

        public static IEnumerable<ConsoleKeyInfo> Keys()
        {
            while (true)
            {
                yield return Console.ReadKey(true);
            }
        }
    }
}

private float totalCount = 0f;
private float countOne = 1f;
private List<DateTime> pressedTime = new List<DateTime>();

void Update(){

    if(Input.GetKeyDown(KeyCode.F)){

        pressedTime.Add(DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
    }

    if (pressedTime.Count == 5){
        if ( (DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")) - pressedTime[0]).Seconds <= 1)
            do stuff
        pressedTime.Remove(0);
    }
}

public float interval = 1f;
public int count = 5;
private Queue<float> timeStamps = new Queue<float>();

void Update () {
    if (Input.GetKeyDown(KeyCode.F)) {
        float secsNow = Time.realtimeSinceStartup;
        timeStamps.Enqueue(secsNow);
        if (timeStamps.Count >= count) {
            float oldest = timeStamps.Dequeue();
            if (secsNow - oldest < interval) {
                Debug.Log("pressed " + count + " times in " + (secsNow - oldest) + "sec");
            }
        }
    }

}