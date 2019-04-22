// ANDREAS added this: In some cases we apparently don't get correct width and height until we have tried to read pixels
    // from the buffer.
    void TryDummySnapshot( ) {
        if(!gotAspect) {
            if( webCamTexture.width>16 ) {
                if( Application.platform == RuntimePlatform.IPhonePlayer ) {
                    if(verbose)Debug.Log("Already got width height of WebCamTexture.");
                } else { 
                    if(verbose)Debug.Log("Already got width height of WebCamTexture. - taking a snapshot no matter what.");
                    var tmpImg = new Texture2D( webCamTexture.width, webCamTexture.height, TextureFormat.RGB24, false );
                    Color32[] c = webCamTexture.GetPixels32();
                    tmpImg.SetPixels32(c);
                    tmpImg.Apply();
                    Texture2D.Destroy(tmpImg);
                }
                gotAspect = true;
            } else {
                if(verbose)Debug.Log ("Taking dummy snapshot");
                var tmpImg = new Texture2D( webCamTexture.width, webCamTexture.height, TextureFormat.RGB24, false );
                Color32[] c = webCamTexture.GetPixels32();
                tmpImg.SetPixels32(c);
                tmpImg.Apply();
                Texture2D.Destroy(tmpImg);
            }
        }
    }

    - (NSString*)pickPresetFromWidth:(int)w height:(int)h
{
static NSString* preset[] =
{
    AVCaptureSessionPreset352x288,
    AVCaptureSessionPreset640x480,
    AVCaptureSessionPreset1280x720,
    AVCaptureSessionPreset1920x1080,
};
static int presetW[] = { 352, 640, 1280, 1920 };

//[AVCamViewController setFlashMode:AVCaptureFlashModeAuto forDevice:[[self videoDeviceInput] device]];


#define countof(arr) sizeof(arr)/sizeof(arr[0])

static_assert(countof(presetW) == countof(preset), "preset and preset width arrrays have different elem count");

int ret = -1, curW = -10000;
for(int i = 0, n = countof(presetW) ; i < n ; ++i)
{
    if( ::abs(w - presetW[i]) < ::abs(w - curW) && [self.captureSession canSetSessionPreset:preset[i]] )
    {
        ret = i;
        curW = presetW[i];
    }
}

NSAssert(ret != -1, @"Cannot pick capture preset");
return ret != -1 ? preset[ret] : AVCaptureSessionPresetHigh;

#undef countof
 }