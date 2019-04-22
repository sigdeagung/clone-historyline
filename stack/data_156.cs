public interface ICoinPickedHandler: IEventSystemHandler
{
    void OnCoinPickedUp(CoinPickedEventData eventData);
}
public class ScoreManager
    : MonoBehaviour, ICoinPickedHandler
{
    private int m_Score;

    public void OnCoinPickedUp(CoinPickedEventData eventData)
    {
        this.m_Score += eventData.Score;
        eventData.Use();
    }

    // code to display score or whatever ...
}
private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        // assuming your collision.gameObject
        // has the ICoinPicker interface implemented
        ExecuteEvents.Execute<ICoinPickedHandler>(collision.gameObject, new CoinPickedEventData(score), (a,b)=>a.OnCoinPickedUp(b));
    }
}
public class CoinPickedEventData
    : BaseEventData
{
    public readonly int Score;

    public CoinPickedEventData(int score)
        : base(EventSystem.current)
    {
        Score = score;
    }
}