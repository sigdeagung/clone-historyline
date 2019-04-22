[DllImport("AudioPluginSpecDelay")]
private static extern void getArray(out int length, out IntPtr array);

int theSize;
IntPtr theArrayPtr;
double[] theArray;

getArray(out theSize, out theArrayPtr);
Marshal.Copy(theArrayPtr, theArray, 0, theSize);
Marshal.FreeCoTaskMem(theArrayPtr);
// C# code
[DllImport("AudioPluginSpecDelay")]
private static extern int getArrayLength(); 

[DllImport("AudioPluginSpecDelay")]
private static extern void getArray(int length,[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] double[] array);

int theSize = getArrayLength();
double[] theArray = new double[theSize];
getArray(theSize, theArray);