protected Vector2 getPagePos(int x, int y)
{
    float halfTile = loadSquares / 2.0f;
    return new Vector2( 
        (-halfTile * tileSize) + (x * tileSize),
        (-halfTile * tileSize) + (y * tileSize)
    );
}

public void generate(int x, int y)
{
    if(x >= loadSquares || x < 0 || y >= loadSquares || y < 0) return;
    t[x,y] = Terrain.CreateTerrainGameObject(new TerrainData()).GetComponent<Terrain>();
    t[x,y].name = "Terrain [" + x + "," + y + "]";
    td[x,y] = t[x,y].terrainData;
    td[x,y].heightmapResolution = resolution;

    tr[x,y] = t[x,y].transform;
    Vector2 pagePos = getPagePos(x, y);
    //Actual data generation happens here.
    int xLim = td[x,y].heightmapWidth;
    int yLim = td[x,y].heightmapHeight;

    float[,] array = new float[xLim, yLim];
    float min = int.MaxValue;
    float max = int.MinValue;
    for(int cx = 0; cx < xLim; cx++)
    {
        for(int cy = 0; cy < yLim; cy++)
        {
            array[cx,cy] = sample(
                new Vector3(
                    (float)cx * (tileSize / (float)(xLim - 1)) + pagePos.x, 
                    0.0f,
                    (float)cy * (tileSize / (float)(yLim - 1)) + pagePos.y
                ) 
            ); 

            if(min > array[cx,cy]) 
                min = array[cx,cy];
            if(max < array[cx,cy]) 
                max = array[cx,cy];

        }
    }

    //Set up the Terrain object to receive the data
    float diff = max != min ? max - min : 10.0f;
    tr[x,y].position = new Vector3(pagePos.y, min, pagePos.x);
    td[x,y].size = new Vector3(tileSize, diff, tileSize);

    //Convert the data to fit in the Terrain object
    for(int cx = 0; cx < xLim; cx++)
    {
        for(int cy = 0; cy < yLim; cy++)
        {
            array[cx,cy] -= min;
            array[cx,cy] /= diff;
        }
    }
    //Set the data in the Terrain object
    td[x,y].SetHeights(0, 0, array);
}