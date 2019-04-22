int lastSample = 0;
void FixedUpdate()
{
    // If there is a connection
    if (Network.connections.Length > 0)
    {
        if (notRecording)
        {
            notRecording = false;
            sendingClip = Microphone.Start(null, true, 100, FREQUENCY);
            sending = true;
        }
        else if(sending)
        {
            int pos = Microphone.GetPosition(null);
            int diff = pos-lastSample;

            if (diff > 0)
            {
                float[] samples = new float[diff * sendingClip.channels];
                sendingClip.GetData (samples, lastSample);
                byte[] ba = ToByteArray (samples);
                networkView.RPC ("Send", RPCMode.Others, ba, sendingClip.channels);
                Debug.Log(Microphone.GetPosition(null).ToString());
            }
            lastSample = pos;
        }
    }
}

[RPC]
public void Send(byte[] ba, int chan) {
    float[] f = ToFloatArray(ba);
    audio.clip = AudioClip.Create("", f.Length, chan, FREQUENCY,true,false);
    audio.clip.SetData(f, 0);
    if (!audio.isPlaying) audio.Play();

}
// Used to convert the audio clip float array to bytes
public byte[] ToByteArray(float[] floatArray) {
    int len = floatArray.Length * 4;
    byte[] byteArray = new byte[len];
    int pos = 0;
    foreach (float f in floatArray) {
        byte[] data = System.BitConverter.GetBytes(f);
        System.Array.Copy(data, 0, byteArray, pos, 4);
        pos += 4;
    }
    return byteArray;
}
// Used to convert the byte array to float array for the audio clip
public float[] ToFloatArray(byte[] byteArray) {
    int len = byteArray.Length / 4;
    float[] floatArray = new float[len];
    for (int i = 0; i < byteArray.Length; i+=4) {
        floatArray[i/4] = System.BitConverter.ToSingle(byteArray, i);
    }
    return floatArray;
}