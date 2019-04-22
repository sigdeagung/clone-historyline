public List<Vector2> Series1Data;
... //I populate the List with some coordinates

MaXValue = Series1Data[0].x; //Get first value
for(int i = 1; i < Series1Data.Count; i++) { //Go throught all entries
  MaXValue = Mathf.Max(Series1Data[i].x, MaXValue); //Always get the maximum value
}
float xMax = Single.MinValue; 
foreach (Vector2 vector in Series1Data)
 {
   if (vector.X > xMax)
   {
     xMax = vector.X; 
   } 
}
public Vector2[] Series1Data;
public float[] Series1DataX;
... //Populate Series1Data with data like this: Series1Data[number] = data;
... //Populate Series1DataX with data like this: Series1DataX[number] = data.x;
MaXValue = Mathf.Max(Series1DataX);

using UnityEngine;
using System.Collections.Generic;

public class LelExample : MonoBehaviour
{
    public List<Vector2> Vec2List = new List<Vector2>();
    public float maxX;

    public void Start()
    {
        float[] x; //set temp arrays
        float[] y;

        GetArraysFromVecList(Vec2List, out x, out y); //set the arrays outputting x and y

        maxX = Mathf.Max(x); //Max the x's
    }
    public void GetArraysFromVecList(List<Vector2> list, out float[] x, out float[] y) //My Custom Void
    {
        float[] tx = new float[list.Count]; //Define temporary arrays
        float[] ty = new float[list.Count];

        for (int i = 0; i < list.Count; ++i)
        {
            tx[i] = list[i].x; //Add x to each corrosponding tx
            ty[i] = list[i].y; //Add y to each corrosponding ty
        }
        x = tx; //set the arrays
        y = ty;
    }
}