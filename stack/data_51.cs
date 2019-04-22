//Create Server Instance
NamedPipeServerStream server = new NamedPipeServerStream("MyCOMApp", PipeDirection.InOut, 1);
//Wait for a client to connect
server.WaitForConnection();
//Created stream for reading and writing
StreamString serverStream = new StreamString(server);
//Send Message to Client
serverStream.WriteString("Hello From Server");
//Read from Client
string dataFromClient = serverStream.ReadString();
UnityEngine.Debug.Log("Received from Client: " + dataFromClient);
//Close Connection
server.Close();
//Create Client Instance
NamedPipeClientStream client = new NamedPipeClientStream(".", "MyCOMApp",
               PipeDirection.InOut, PipeOptions.None,
               TokenImpersonationLevel.Impersonation);

//Connect to server
client.Connect();
//Created stream for reading and writing
StreamString clientStream = new StreamString(client);
//Read from Server
string dataFromServer = clientStream.ReadString();
UnityEngine.Debug.Log("Received from Server: " + dataFromServer);
//Send Message to Server
clientStream.WriteString("Bye from client");
//Close client
client.Close();
public class StreamString
{
    private Stream ioStream;
    private UnicodeEncoding streamEncoding;

    public StreamString(Stream ioStream)
    {
        this.ioStream = ioStream;
        streamEncoding = new UnicodeEncoding();
    }

    public string ReadString()
    {
        int len = 0;

        len = ioStream.ReadByte() * 256;
        len += ioStream.ReadByte();
        byte[] inBuffer = new byte[len];
        ioStream.Read(inBuffer, 0, len);

        return streamEncoding.GetString(inBuffer);
    }

    public int WriteString(string outString)
    {
        byte[] outBuffer = streamEncoding.GetBytes(outString);
        int len = outBuffer.Length;
        if (len > UInt16.MaxValue)
        {
            len = (int)UInt16.MaxValue;
        }
        ioStream.WriteByte((byte)(len / 256));
        ioStream.WriteByte((byte)(len & 255));
        ioStream.Write(outBuffer, 0, len);
        ioStream.Flush();

        return outBuffer.Length + 2;
    }
}