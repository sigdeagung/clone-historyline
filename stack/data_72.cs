PassInFoo( new Foo { Bar = false } );
var tmp = new Foo();    //Bar initialized to true
tmp.Bar = false;
PassInFoo( tmp );
using UnityEngine;
using System.Collections;

public class Foo1 {
    public bool Bar=false;
}

public class Foo2 {
    public bool Bar=true;
}

public class Foo1i {
    public int Bar=0;
}

public class Foo2i {
    public int Bar=42;
}

public class PropTest:MonoBehaviour {

    void Start() {
        PassInFoo(new Foo1 {Bar=true}); // FOO1: True (OK)
        PassInFoo(new Foo2 {Bar=false});/// FOO2: True (FAIL!)
        PassInFoo(new Foo1i {Bar=42});  // FOO1i: 42 (OK)
        PassInFoo(new Foo2i {Bar=0});/// FOO2i: 42 (FAIL!)
        PassInFoo(new Foo2i {Bar=13});/// FOO2i: 13 (OK)
    }

    void PassInFoo(Foo1 f) {Debug.Log("FOO1: "+f.Bar);}

    void PassInFoo(Foo2 f) {Debug.Log("FOO2: "+f.Bar);}

    void PassInFoo(Foo1i f) {Debug.Log("FOO1i: "+f.Bar);}

    void PassInFoo(Foo2i f) {Debug.Log("FOO2i: "+f.Bar);}
}
class Program
{
    static void Main(string[] args)
    {
        PassInFoo( new Foo { Bar = false } );
    }
    public class Foo
    {
        public bool Bar = true;
    }

    public static void PassInFoo(Foo obj)

    {
        Console.WriteLine(obj.Bar.ToString());
        Console.ReadLine();
    }
}