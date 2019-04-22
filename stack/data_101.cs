int counter = 0;
byte[] completeImageByte = new byte[500000];
byte[] receivedBytes = new byte[500000];
receivedBytes[counter] = byteFromCamera;
counter++;
Buffer.BlockCopy(receivedBytes, 0, completeImageByte, 0, counter);
counter = 0;
public RawImage screenDisplay;
if(updateFrame){
Texture2D camTexture = new Texture2D(2, 2);
camTexture.LoadImage(completeImageByte);
screenDisplay.texture = camTexture;
}
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CustomWebRequest : DownloadHandlerScript
{    
    // Standard scripted download handler - will allocate memory on each ReceiveData callback
    public CustomWebRequest()
        : base()
    {
    }

    // Pre-allocated scripted download handler
    // Will reuse the supplied byte array to deliver data.
    // Eliminates memory allocation.
    public CustomWebRequest(byte[] buffer)
        : base(buffer)
    {
    }

    // Required by DownloadHandler base class. Called when you address the 'bytes' property.
    protected override byte[] GetData() { return null; }

    // Called once per frame when data has been received from the network.
    protected override bool ReceiveData(byte[] byteFromCamera, int dataLength)
    {
        if (byteFromCamera == null || byteFromCamera.Length < 1)
        {
            //Debug.Log("CustomWebRequest :: ReceiveData - received a null/empty buffer");
            return false;
        }

        //Search of JPEG Image here

        return true;
    }

    // Called when all data has been received from the server and delivered via ReceiveData
    protected override void CompleteContent()
    {
        //Debug.Log("CustomWebRequest :: CompleteContent - DOWNLOAD COMPLETE!");
    }

    // Called when a Content-Length header is received from the server.
    protected override void ReceiveContentLength(int contentLength)
    {
        //Debug.Log(string.Format("CustomWebRequest :: ReceiveContentLength - length {0}", contentLength));
    }
}
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Test : MonoBehaviour
{

    CustomWebRequest camImage;
    UnityWebRequest webRequest;
    byte[] bytes = new byte[90000];

    void Start()
    {
        string url = "http://camUrl/mjpg/video.mjpg";
        webRequest = new UnityWebRequest(url);
        webRequest.downloadHandler = new CustomWebRequest(bytes);
        webRequest.Send();
    }
}