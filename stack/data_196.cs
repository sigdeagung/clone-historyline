class levelManager {
    private static readonly Dictionary<object, levelManager> _instances = new Dictionary<object, levelManager>();

    private levelManager() {
    }

    public static levelManager GetInstance(object key) { // unique key for your stage
        lock (_instances) {   
            levelManager instance;
            if (!_instances.TryGetValue(key, out instance)) {
                instance = new levelManager();
                _instances.Add(key, instance);
            }
            return instance;
        }
    }
}

public class LevelManagerSpawner
{
    public GameObject[] LevelManagerPrefabs;

    void OnLevelWasLoaded( int levelNumber )
    {
        Instantiate( LevelManagerPrefabs[ levelNumber ] );
    }
}