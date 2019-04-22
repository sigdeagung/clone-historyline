private static byte[] ToByteArray<T>(T[] source) where T : struct
{
    GCHandle handle = GCHandle.Alloc(source, GCHandleType.Pinned);
    try
    {
        IntPtr pointer = handle.AddrOfPinnedObject();
        byte[] destination = new byte[source.Length * Marshal.SizeOf(typeof(T))];
        Marshal.Copy(pointer, destination, 0, destination.Length);
        return destination;
    }
    finally
    {
        if (handle.IsAllocated)
            handle.Free();
    }
}

private static T[] FromByteArray<T>(byte[] source) where T : struct
{
    T[] destination = new T[source.Length / Marshal.SizeOf(typeof(T))];
    GCHandle handle = GCHandle.Alloc(destination, GCHandleType.Pinned);
    try
    {
        IntPtr pointer = handle.AddrOfPinnedObject();
        Marshal.Copy(source, 0, pointer, source.Length);
        return destination;
    }
    finally
    {
        if (handle.IsAllocated)
            handle.Free();
    }
}
[StructLayout(LayoutKind.Sequential)]
public struct Demo
{
    public double X;
    public double Y;
}

private static void Main()
{
    Demo[] array = new Demo[2];
    array[0] = new Demo { X = 5.6, Y = 6.6 };
    array[1] = new Demo { X = 7.6, Y = 8.6 };

    byte[] bytes = ToByteArray(array);
    Demo[] array2 = FromByteArray<Demo>(bytes);
}
public class MemCopyUtils
{
    unsafe delegate void MemCpyDelegate(byte* dst, byte* src, int len);
    static MemCpyDelegate MemCpy;

    static MemCopyUtils()
    {
        InitMemCpy();
    }

    static void InitMemCpy()
    {
        var mi = typeof(Buffer).GetMethod(
            name: "Memcpy",
            bindingAttr: BindingFlags.NonPublic | BindingFlags.Static,
            binder:  null,
            types: new Type[] { typeof(byte*), typeof(byte*), typeof(int) },
            modifiers: null);
        MemCpy = (MemCpyDelegate)Delegate.CreateDelegate(typeof(MemCpyDelegate), mi);
    }

    public unsafe static Color32[] ByteArrayToColor32Array(byte[] bytes)
    {
        Color32[] colors = new Color32[bytes.Length / sizeof(Color32)];

        fixed (void* tempC = &colors[0])
        fixed (byte* pBytes = bytes)
        {
            byte* pColors = (byte*)tempC;
            MemCpy(pColors, pBytes, bytes.Length);
        }
        return colors;
    }
}
img = new Color32[byteArray.Length / nChannels]; //nChannels being 4
Parallel.For(0, img.Length, i =>
{
    img[i].r = byteArray[i*nChannels];
    img[i].g = byteArray[i*nChannels+1];
    img[i].b = byteArray[i*nChannels+2];
    img[i].a = byteArray[i*nChannels+3];
});
img = new Color32[byteArray.Length / nChannels]; //nChannels being 4
fixed (byte* ba = byteArray)
{
    fixed (Color32* c = img)
    {
        byte* byteArrayPtr = ba;
        Color32* colorPtr = c;
        for (int i = 0; i < img.Length; i++)
        {
            (*colorPtr).r = *byteArrayPtr++;
            (*colorPtr).g = *byteArrayPtr++;
            (*colorPtr).b = *byteArrayPtr++;
            (*colorPtr).a = *byteArrayPtr++;
            colorPtr++;
        }
    }
}
public Color32[] GetColorArray(byte[] myByte)
{
    if (myByte.Length % 1 != 0) 
       throw new Exception("Must have an even length");

    var colors = new Color32[myByte.Length / nChannels];

    for (var i = 0; i < myByte.Length; i += nChannels)
    {
       colors[i / nChannels] = new Color32(
           (byte)(myByte[i] & 0xF8),
           (byte)(((myByte[i] & 7) << 5) | ((myByte[i + 1] & 0xE0) >> 3)),
           (byte)((myByte[i + 1] & 0x1F) << 3),
           (byte)1);
    }

    return colors;
}