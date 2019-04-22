public class Checkpoint : MonoBehaviour
{
    //used for locking
    private readonly object ThreadLocker = new Object();

    private List<Stat> storedStats = new List<Stat>();

    public List<State> GetStoredStatsCopy()
    {
        lock(ThreadLocker)
        {
            //makes a copy/snapshot of the list
            return new List<Stat>(storedStats);
        }
    }

    //sample code that changes storedStats
    public void AddStat(Stat newStat)
    {
        lock(ThreadLocker)
        {
            storedStats.Add(newStat);
        }
    }
}