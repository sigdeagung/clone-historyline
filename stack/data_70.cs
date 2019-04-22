void Awake () 
{
    StartCoroutine(doSetup());
}

IEnumerator doSetup ()
{
    yield return StartCoroutine(createGrid()); // will yield until createGrid is finished
    doOtherStuff();   
}

IEnumerator createGrid()
{   
        for (int i = 0; i < Constants.BOARD_WIDTH; i++)             
        {
            for (int j = 0; j < Constants.BOARD_HEIGHT; j++) 
            {
                // Instantiate a new tile 
                yield return(0);
            }
        }
    }
}