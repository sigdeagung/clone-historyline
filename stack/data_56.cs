using UnityEngine;

public class ServiceManager : MonoBehaviour
{
    // If this T confuses you from the generic T used elsewhere, rename it
    public static Transform T { get; private set; }

    void Awake()
    {
        T = transform;
    }

    public T Provide<T>() where T : MonoBehaviour
    {
        return ServiceMap<T>.service; // no cast required
    }
}

static class ServiceMap<T> where T : MonoBehaviour
{
    public static readonly T service;

    static ServiceMap()
    {
        // Create service
        GameObject serviceObject = new GameObject(typeof(T).Name);
        serviceObject.transform.SetParent(ServiceManager.T); // make service GO our child
        service = serviceObject.AddComponent<T>(); // attach service to GO
    }
}

public class ServiceTest : MonoBehaviour
{
    private void Start()
    {
        // no need to Create services
        // They will be created when Provide is first called on them
        // Though if you want them up and running at Start, call Provide
        // on each here.
    }

    private void Example()
    {
        // Get a service
        ServiceManager services = FindObjectOfType<ServiceManager>();
        MapService map = services.Provide<MapService>();
        // do whatever you want with map
    }
}