public class MainComponentManger {
    private static MainComponentManger instance;
    public static void CreateInstance () {
        if (instance == null) {
            instance = new MainComponentManger ();
            GameObject go = GameObject.Find ("Main");
            if (go == null) {
                go = new GameObject ("Main");
                instance.main = go;
                // important: make game object persistent:
                Object.DontDestroyOnLoad (go);
            }
            // trigger instantiation of other singletons
            Component c = MenuManager.SharedInstance;
            // ...
        }
    }

    GameObject main;

    public static MainComponentManger SharedInstance {
        get {
            if (instance == null) {
                CreateInstance ();
            }
            return instance;
        }
    }

    public static T AddMainComponent <T> () where T : UnityEngine.Component {
        T t = SharedInstance.main.GetComponent<T> ();
        if (t != null) {
            return t;
        }
        return SharedInstance.main.AddComponent <T> ();
    }

    public class AudioManager : MonoBehaviour {
    private static AudioManager instance = null;
    public static AudioManager SharedInstance {
        get {
            if (instance == null) {
                instance = MainComponentManger.AddMainComponent<AudioManager> ();
            }
            return instance;
        }
    }