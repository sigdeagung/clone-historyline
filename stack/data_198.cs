using UnityEngine;
using System.Collections;

public class SPECTRUMANALYZER : MonoBehaviour 
{
    void Update( )
    {
        float[] spectrum = new float[1024];
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular );
        float l1 = spectrum [0] + spectrum [2] + spectrum [4];
        float l2 = spectrum [10] + spectrum [11] + spectrum [12];
        float l3 = spectrum[20] + spectrum [21] + spectrum [22];
        float l4 = spectrum [40] + spectrum [41] + spectrum [42] + spectrum [43];
        float l5 = spectrum [80] + spectrum [81] + spectrum [82] + spectrum [83];
        float l6 = spectrum [160] + spectrum [161] + spectrum [162] + spectrum [163];
        float l7 = spectrum [320] + spectrum [321] + spectrum [322] + spectrum [323];
        Debug.Log(l7);
        GameObject [] cubes = GameObject.FindGameObjectsWithTag("CUBE");

    for( int i = 1; i < cubes.Length; i++ )
    {
        switch (i)
        {
            case 1:
                cubes[i].gameObject.transform.localScale = new Vector3(1, l1 * 100, 0.5f); // base drum
                break;
            case 2:
                cubes[i].gameObject.transform.localScale = new Vector3(1, l2 * 200, 0.5f); // base guitar
                break;
            case 3:
                cubes[i].gameObject.transform.localScale = new Vector3(1, l3 * 400, 0.5f);
                break;
            case 4:
                cubes[i].gameObject.transform.localScale = new Vector3(1, l4 * 800, 0.5f);
                break;
            case 5:
                cubes[i].gameObject.transform.localScale = new Vector3(1, l5 * 1600, 0.5f);
                break;
            case 6:
                cubes[i].gameObject.transform.localScale = new Vector3(1, l6 * 3200, 0.5f);
                break;
            case 7:
                cubes[i].gameObject.transform.localScale = new Vector3(1, l7 * 6400, 0.5f); //*tsk tsk tsk
                break;
        }           
    }
}