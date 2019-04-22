public class ComponentPool
{
    //Determines if pool should expand when no pool is available or just return null
    public bool autoExpand = true;
    //Links the type of the componet with the component
    Dictionary<Type, List<Component>> poolTypeDict = new Dictionary<Type, List<Component>>();

    public ComponentPool() { }


    //Adds Prefab component to the ComponentPool
    public void AddPrefab<T>(T prefabReference, int count = 1)
    {
        _AddComponentType<T>(prefabReference, count);
    }

    private Component _AddComponentType<T>(T prefabReference, int count = 1)
    {
        Type compType = typeof(T);

        if (count <= 0)
        {
            Debug.LogError("Count cannot be <= 0");
            return null;
        }

        //Check if the component type already exist in the Dictionary
        List<Component> comp;
        if (poolTypeDict.TryGetValue(compType, out comp))
        {
            if (comp == null)
                comp = new List<Component>();

            //Create the type of component x times
            for (int i = 0; i < count; i++)
            {
                //Instantiate new component and UPDATE the List of components
                Component original = (Component)Convert.ChangeType(prefabReference, typeof(T));
                Component instance = Instantiate(original);
                //De-activate each one until when needed
                instance.gameObject.SetActive(false);
                comp.Add(instance);
            }
        }
        else
        {
            //Create the type of component x times
            comp = new List<Component>();
            for (int i = 0; i < count; i++)
            {
                //Instantiate new component and UPDATE the List of components
                Component original = (Component)Convert.ChangeType(prefabReference, typeof(T));
                Component instance = Instantiate(original);
                //De-activate each one until when needed
                instance.gameObject.SetActive(false);
                comp.Add(instance);
            }
        }

        //UPDATE the Dictionary with the new List of components
        poolTypeDict[compType] = comp;

        /*Return last data added to the List
         Needed in the GetAvailableObject function when there is no Component
         avaiable to return. New one is then created and returned
         */
        return comp[comp.Count - 1];
    }


    //Get available component in the ComponentPool
    public T GetAvailableObject<T>(T prefabReference)
    {
        Type compType = typeof(T);

        //Get all component with the requested type from  the Dictionary
        List<Component> comp;
        if (poolTypeDict.TryGetValue(compType, out comp))
        {
            //Get de-activated GameObject in the loop
            for (int i = 0; i < comp.Count; i++)
            {
                if (!comp[i].gameObject.activeInHierarchy)
                {
                    //Activate the GameObject then return it
                    comp[i].gameObject.SetActive(true);
                    return (T)Convert.ChangeType(comp[i], typeof(T));
                }
            }
        }

        //No available object in the pool. Expand array if enabled or return null
        if (autoExpand)
        {
            //Create new component, activate the GameObject and return it
            Component instance = _AddComponentType<T>(prefabReference, 1);
            instance.gameObject.SetActive(true);
            return (T)Convert.ChangeType(instance, typeof(T));
        }
        return default(T);
    }
}

public static class ExtensionMethod
{
    public static void RecyclePool(this Component component)
    {
        //Reset position and then de-activate the GameObject of the component
        GameObject obj = component.gameObject;
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
        component.gameObject.SetActive(false);
    }
}
// pool of single object type, uses new for instantiation
public class ObjectPool<T> where T : new()
{
    // this will hold all the instances, notice that it's up to caller to make sure
    // the pool size is big enough not to reuse an object that's still in use
    private readonly T[] _pool = new T[_maxObjects];
    private int _current = 0;

    public ObjectPool()
    {
        // performs initialization, one may consider doing lazy initialization afterwards
        for (int i = 0; i < _maxObjects; ++i)
            _pool[i] = new T();
    }

    private const int _maxObjects = 100;  // Set this to whatever

    public T Get()
    {
        return _pool[_current++ % _maxObjects];
    }
}

// pool of generic pools
public class PoolPool
{
    // this holds a reference to pools of known (previously used) object pools
    // I'm dissatisfied with an use of object here, but that's a way around the generics :/
    private readonly Dictionary<Type, object> _pool = new Dictionary<Type, object>();

    public T Get<T>() where T : new()
    {
        // is the pool already instantiated?
        if (_pool.TryGetValue(typeof(T), out var o))
        {
            // if yes, reuse it (we know o should be of type ObjectPool<T>,
            // where T matches the current generic argument
            return ((ObjectPool<T>)o).Get();
        }

        // First time we see T, create new pool and store it in lookup dictionary
        // for later use
        ObjectPool<T> pool = new ObjectPool<T>();
        _pool.Add(typeof(T), pool);

        return pool.Get();
    }
}