WWW www = new WWW("file://" + filePath);
yield return www;
///////m_myTexture = www.texture;  // Commenting this line removes frame out
www.LoadImageIntoTexture(m_myTexture);
//Create new Empty texture with size that matches source info
m_myTexture = new Texture2D(www.texture.width, www.texture.height, www.texture.format, false);
Graphics.CopyTexture(www.texture, m_myTexture);
using (UnityWebRequest www = UnityWebRequest.GetTexture("http://www.my-server.com/image.png"))
{
    yield return www.Send();
    if (www.isError)
    {
        Debug.Log(www.error);
    }
    else
    {
        m_myTexture = DownloadHandlerTexture.GetContent(www);
    }
}
public Texture2D m_myTexture;

void Start()
{
    //Application.runInBackground = true;
    StartCoroutine(downloadTexture());
}

IEnumerator downloadTexture()
{
    //http://visibleearth.nasa.gov/view.php?id=79793
    //http://eoimages.gsfc.nasa.gov/images/imagerecords/79000/79793/city_lights_africa_8k.jpg

    string url = "http://eoimages.gsfc.nasa.gov/images/imagerecords/79000/79793/city_lights_africa_8k.jpg";
    //WWW www = new WWW("file://" + filePath);
    WWW www = new WWW(url);
    yield return www;

    //m_myTexture = www.texture;  // Commenting this line removes frame out

    Debug.Log("Downloaded Texture. Now copying it");

    //Copy Texture to m_myTexture WITHOUT callback function
    //StartCoroutine(copyTextureAsync(www.texture));

    //Copy Texture to m_myTexture WITH callback function
    StartCoroutine(copyTextureAsync(www.texture, false, finishedCopying));
}


IEnumerator copyTextureAsync(Texture2D source, bool useMipMap = false, System.Action callBack = null)
{

    const int LOOP_TO_WAIT = 400000; //Waits every 400,000 loop, Reduce this if still freezing
    int loopCounter = 0;

    int heightSize = source.height;
    int widthSize = source.width;

    //Create new Empty texture with size that matches source info
    m_myTexture = new Texture2D(widthSize, heightSize, source.format, useMipMap);

    for (int y = 0; y < heightSize; y++)
    {
        for (int x = 0; x < widthSize; x++)
        {
            //Get color/pixel at x,y pixel from source Texture
            Color tempSourceColor = source.GetPixel(x, y);

            //Set color/pixel at x,y pixel to destintaion Texture
            m_myTexture.SetPixel(x, y, tempSourceColor);

            loopCounter++;

            if (loopCounter % LOOP_TO_WAIT == 0)
            {
                //Debug.Log("Copying");
                yield return null; //Wait after every LOOP_TO_WAIT 
            }
        }
    }
    //Apply changes to the Texture
    m_myTexture.Apply();

    //Let our optional callback function know that we've done copying Texture
    if (callBack != null)
    {
        callBack.Invoke();
    }
}

void finishedCopying()
{
    Debug.Log("Finished Copying Texture");
    //Do something else
}