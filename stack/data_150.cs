using UnityEngine;
using System.Collections;

public class HiResScreenShots : MonoBehaviour {
    public int resWidth = 2550; 
    public int resHeight = 3300;

    private bool takeHiResShot = false;

    public static string ScreenShotName(int width, int height) {
        return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png", 
                             Application.dataPath, 
                             width, height, 
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void TakeHiResShot() {
        takeHiResShot = true;
    }

    void LateUpdate() {
        takeHiResShot |= Input.GetKeyDown("k");
        if (takeHiResShot) {
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            camera.targetTexture = rt;
            Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            camera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            camera.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors
            Destroy(rt);
            byte[] bytes = screenShot.EncodeToPNG();
            string filename = ScreenShotName(resWidth, resHeight);
            System.IO.File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("Took screenshot to: {0}", filename));
            takeHiResShot = false;
        }
    }
}

using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour 
{
    void OnMouseDown() 
    {
        Application.CaptureScreenshot("Screenshot.png");
    }
}

internal class ScreenCapture : MonoBehaviour
{
    internal byte[] Image = null;

    internal void SaveScreenshot()
    {
        StartCoroutine(SaveScreenshot_ReadPixelsAsynch());
    }

    private IEnumerator SaveScreenshot_ReadPixelsAsynch()
    {
        //Wait for graphics to render
        yield return new WaitForEndOfFrame();

        //Create a texture to pass to encoding
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        //Put buffer into texture
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);

        //Split the process up--ReadPixels() and the GetPixels() call inside of the encoder are both pretty heavy
        yield return 0;

        byte[] bytes = texture.EncodeToPNG();
        Image = bytes;

        //Tell unity to delete the texture, by default it seems to keep hold of it and memory crashes will occur after too many screenshots.
        DestroyObject(texture);
    }
}