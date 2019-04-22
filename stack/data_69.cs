Stopwatch watch = new Stopwatch();
watch.Start();
// do some things here
// output the elapse if needed
watch = Stopwatch.StartNew(); // creates a new Stopwatch instance 
                              // and starts it upon creation

public static class ExtensionMethods
{
    public static void Restart(this Stopwatch watch)
    {
        watch.Stop();
        watch.Start();
    }
}
class Program
{
    static void Main(string[] args)
    {
        Stopwatch watch = new Stopwatch();
        watch.Restart(); // an extension method
    }
}
public static class StopwatchExtensions
  {
    /// <summary>
    /// Support for .NET Framework <= 3.5
    /// </summary>
    /// <param name="sw"></param>
    public static void Restart(this Stopwatch sw)
    {
      sw.Stop();
      sw.Reset();
      sw.Start();
    }
  }