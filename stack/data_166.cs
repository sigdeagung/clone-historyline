public class GuiController : MonoBehaviour
{
    //singleton setup
    static GuiController instance;
    void Awake() { instance = this; }

    //now it's ready for static calls

    public UnityEngine.UI.Text MyText;

    //static method which you can call from anywhere
    public static void SetMyText(string txt) { instance.MyText.text = txt; }
}
public class GameController : MonoBehaviour
{
    static GameController instance;
    void Awake() { instance = this; }

    float score;
    public static float Score { get { return instance.score; } set { instance.score = value; } }
}