using UnityEngine;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;


class UnManagedInterop : MonoBehaviour {
  private delegate int Callback(string text);
  private Callback mInstance;   // Ensure it doesn't get garbage collected


  public void Test() {
        mInstance = new Callback(Handler);
        SetCallback(mInstance);
        TestCallback();
  }

  private int Handler(string text) {
    // Do something...
    print(text);
    return 42;
  }

  [DllImport("test0")]
  private static extern void SetCallback(Callback fn);
  [DllImport("test0")]
  private static extern void TestCallback();

    void Start()
    {
        Thread oThread = new Thread(new ThreadStart(Test));

        // Start the thread
        oThread.Start();


    }
}

// C#
using System.Runtime.InteropServices;

class Demo {
    delegate int MyCallback1 (int a, int b);

    [DllImport ("MyRuntime")]
    extern static void RegisterCallback (MyCallback1 callback1);

    static int Add (int a, int b) { return a + b; }
    static int Sub (int a, int b) { return a - b; }

    void Init ()
    {
        // This one registers the method "Add" to be invoked back by C code
        RegisterCallback (Add);
    }
}


// C
typedef int (*callback_t) (int a, int b);
static callback_t my_callback;

void RegisterCallback (callback_t cb)
{
    my_callback = cb;
}

int InvokeManagedCode (int a, int b)
{
    if (my_callback == NULL){
         printf ("Managed code has not initialized this library yet");
         abort ();
    }
    return (*my_callback) (a, b);
}