#define uwp
//#define uwp_build

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using System.Threading;
using System.Linq;



#if uwp
#if !uwp_build
using Windows.Networking;
using Windows.Networking.Sockets;
using UnityEngine.Networking;
using Windows.Foundation;
#endif
#else
using System.Net;
using System.Net.Sockets;
#endif

public class Renderer : MonoBehavior {

byte[] bytes = new byte[8192];
bool ready = false;
const int localPort = 11000;

static bool clientConnected;

#if !uwp

    TcpListener listener;
    private Socket client = null;

#else
#if !uwp_build

    StreamSocketListener socketListener;
    StreamSocket socket;
#endif
#endif

#if !uwp_build
    async
#endif
    void Start()
    {
        clientConnected = false;

#if !uwp
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        listener = new TcpListener(localAddr, localPort);
        listener.Start();
        Debug.Log("Started!");

#else
#if !uwp_build
        socketListener = new StreamSocketListener();


        socketListener.ConnectionReceived += OnConnection;

        await socketListener.BindServiceNameAsync("11000");

}
#endif
#endif

void Update()
    {
#if !uwp
        if (listener.Pending())
        {
            client = listener.AcceptSocket();
            clientConnected = true;
        }
#endif
        // An incoming connection needs to be processed.  
        //don't do anything if the client isn't connected
        if (clientConnected)
        {



            int bytesRec = 0;
#if !uwp
            bytesRec = client.Receive(bytes);
#else
#if !uwp_build
            Stream streamIn = socket.InputStream.AsStreamForRead();

            bytesRec = streamIn.Read(bytes, 0, 8192);
            Debug.Log(bytesRec);
#endif


#endif
            byte[] relevant_bytes = bytes.Take(bytesRec).ToArray();

            //do something with these relevant_bytes

    }
#if uwp
#if !uwp_build
    private async void OnConnection(
        StreamSocketListener sender,
        StreamSocketListenerConnectionReceivedEventArgs args)
    {
        String statusMsg = "Received connection on port: " + args.Socket.Information.LocalPort;
        Debug.Log(statusMsg);
        this.socket = args.Socket;
        clientConnected = true;
    }
#endif
#endif

}
// Initializing the Transport Layer with no arguments (default settings)
NetworkTransport.Init();

// An example of initializing the Transport Layer with custom settings
GlobalConfig gConfig = new GlobalConfig();
gConfig.MaxPacketSize = 500;
NetworkTransport.Init(gConfig);

ConnectionConfig config = new ConnectionConfig();
// use QosType.Reliable if you need TCP
int myReiliableChannelId  = config.AddChannel(QosType.Reliable);
// use QosType.Unreliable if you need UDP
int myUnreliableChannelId = config.AddChannel(QosType.Unreliable);

HostTopology topology = new HostTopology(config, 10);
// set listen port 8888
int hostId = NetworkTransport.AddHost(topology, 8888);

void Update()
{
    int recHostId; 
    int connectionId; 
    int channelId; 
    byte[] recBuffer = new byte[1024]; 
    int bufferSize = 1024;
    int dataSize;
    byte error;
    NetworkEventType recData = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, recBuffer, bufferSize, out dataSize, out error);
    switch (recData)
    {
        case NetworkEventType.Nothing:         //1
            break;
        case NetworkEventType.ConnectEvent:    //2
            break;
        case NetworkEventType.DataEvent:       //3
            break;
        case NetworkEventType.DisconnectEvent: //4
            break;
    }
}