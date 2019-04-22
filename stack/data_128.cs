public T GetComponentOfType<T>()
{
    return (T) GetComponentOfType(typeof(T));
}
// assuming
public class GameObject
{
    List<Component> m_Components; // all components attached to this GameObject
    public TComponent GetComponentOfType<TComponent>()
        where TComponent : Component
    {
        // Select only the ones of type TComponent and return first one
        return m_Components.OfType<TComponent>().FirstOrDefault();
    }
}
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Base-Class
/// </summary>
public abstract class Thing
{
    private ICollection<Thing> _things;

    public Thing()
    {
        _things = new List<Thing>();
    }

    public void AddSomething(Thing toAdd)
    {
        _things.Add(toAdd);
    }

    /// <summary>
    /// Assuming that every type can only appear once
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public T GetComonentOfType<T>() where T : Thing
    {
        return this.GetComonentOfType(typeof(T)) as T;
    }

    public Thing GetComonentOfType(Type t)
    {
        return _things.Where(x => x.GetType() == t).Single();
    }

}

/// <summary>
/// One possible implementation
/// </summary>
public class SpecialThing : Thing
{

}
public abstract class IComponent
{
    protected IList<object> objComponents;

    public object GetComponentOfType(Type type)
    {
        if (objComponents == null)
            return null;

        return objComponents.Where(obj => obj.GetType() == type).FirstOrDefault();
    }
}
public abstract class BaseComponent : IComponent
{
    public object GetComponentOfType<T>()
    {
        return GetComponentOfType(typeof(T));
    }
}