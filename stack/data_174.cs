using UnityEngine;
using System.Collections;
using System;

public class LowPassFilter : MonoBehaviour {

    public float cutoff;
    public float resonance;

    const float CUTOFF_MAX = 128.0f;
    const float CUTOFF_MIN = 0.0f;
    const float RESONANCE_MAX = 128.0f;
    const float RESONANCE_MIN = 0.0f;

    float c;
    float r;
    float v0;
    float v1;

    int sampleRate;

    void Start()
    {
        cutoff = 20.0f;
        resonance = 0.0f;

        c = 0.0f;
        r = 0.0f;
        v0 = 0.0f;
        v1 = 0.0f;

        sampleRate = AudioSettings.outputSampleRate;
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        cutoff = Mathf.Clamp(cutoff, CUTOFF_MIN, CUTOFF_MAX);
        resonance = Mathf.Clamp(resonance, RESONANCE_MIN, RESONANCE_MAX);

        c = Mathf.Pow(0.5f, (128.0f - cutoff) / 16.0f);
        r = Mathf.Pow(0.5f, (resonance + 24.0f) / 16.0f);

        for (int i = 0; i < data.Length; i++)
        {
            v0 =  ((1.0f - r * c) * v0)  -  (c * v1)  + (c * data[i]);
            v1 =  ((1.0f - r * c) * v1)  +  (c * v0);

            data[i] = Mathf.Clamp(v1, -1.0f, 1.0f);
        }
    }
}