int finalRowCount = height - slots + squadMembers.Count;
for (int rowNum = 0 ; rowNum < height && newpositions.Count < squadMembers.Count; rowNum++) {
    for (int i = 0 ; i < rowNum+1 && newpositions.Count < squadMembers.Count ; i++ ) {
        float xOffset = 0f;

        if (rowNum+1 == height) {
            // If we're in the last row, stretch it ...
            if (finalRowCount !=1) {
                // Unless there's only one item in the last row. 
                // If that's the case, leave it centered.

                xOffset = Mathf.Lerp(
                        rowNum/2f,
                        -rowNum/2f,
                        i/(finalRowCount-1f)
                        ) * horizontalModifier;
            }
        }
        else {
            xOffset = (i-rowNum /2f) * horizontalModifier; 
        }

        float yOffset = (float)rowNum * verticalModifier; 

        Vector3 position = new Vector3(
                startPos.x + xOffset, 0f, startPos.y - yOffset);
        newpositions.Add(position);

    }
}