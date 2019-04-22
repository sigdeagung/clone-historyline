if (Input.GetKey (KeyCode.Escape)) 
{
    if (webCameraTexture != null && webCameraTexture.isPlaying)
    {
        Debug.Log("Camera is still playing");
        webCameraTexture.Pause();     

        while (webCameraTexture.isPlaying)
        {
            yield return null;
        }

        Debug.Log("Camera stopped playing");
    }

    webCameraTexture = null;
    Application.LoadLevel("Game");
}

if (Input.GetKey (KeyCode.Escape)) 
{
    while (webCameraTexture!=null && webCameraTexture.isPlaying)
    {
        Debug.Log("is still playing");
        webCameraTexture.Pause();
        webCameraTexture=null;
        break;
    }

    Debug.Log("stoped playing");
    Application.LoadLevel("Game");
}