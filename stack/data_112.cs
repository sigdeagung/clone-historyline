/// <summary>
/// Creates a deep clone of an object using serialization.
/// </summary>
/// <typeparam name="T">The type to be cloned/copied.</typeparam>
/// <param name="o">The object to be cloned.</param>
public static T DeepClone<T>(this T o)
{
    using (MemoryStream stream = new MemoryStream())
    {
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, o);
        stream.Position = 0;
        return (T)formatter.Deserialize(stream);
    }
}
class A
{
    // copy constructor
    public A(A copy) {}
}

// A referenced class implementing 
class B : IDeepCopy
{
    object Copy() { return new B(); }
}

class C : IDeepCopy
{
    A A;
    B B;
    object Copy()
    {
        C copy = new C();

        // copy property by property in a appropriate way
        copy.A = new A(this.A);
        copy.B = this.B.Copy();
     }
}