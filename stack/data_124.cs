//Convert Layer Name to Layer Number
int cubeLayerIndex = LayerMask.NameToLayer("cube");

//Check if layer is valid
if (cubeLayerIndex == -1)
{
    Debug.LogError("Layer Does not exist");
}
else
{
    //Calculate layermask to Raycast to. (Raycast to "cube" layer only)
    int layerMask = (1 << cubeLayerIndex);

    Vector3 fwd = transform.TransformDirection(Vector3.forward);

    //Raycast with that layer mask
    if (Physics.Raycast(transform.position, fwd, 10, layerMask))
    {

    }
}
//Convert Layer Name to Layer Number
int cubeLayerIndex = LayerMask.NameToLayer("cube");
int sphereLayerIndex = LayerMask.NameToLayer("sphere");

//Calculate layermask to Raycast to. (Raycast to "cube" && "sphere" layers only)
int layerMask = (1 << cubeLayerIndex) | (1 << sphereLayerIndex);
//Convert Layer Name to Layer Number
int cubeLayerIndex = LayerMask.NameToLayer("cube");

//Calculate layermask to Raycast to. (Ignore "cube" layer)
int layerMask = (1 << cubeLayerIndex);
//Invert to ignore it
layerMask = ~layerMask;
//Convert Layer Name to Layer Number
int cubeLayerIndex = LayerMask.NameToLayer("cube");
int sphereLayerIndex = LayerMask.NameToLayer("sphere");

//Calculate layermask to Raycast to. (Ignore "cube" && "sphere" layers)
int layerMask = (1 << cubeLayerIndex);
layerMask |= (1 << sphereLayerIndex);
layerMask |= (1 << otherLayerToIgnore1);
layerMask |= (1 << otherLayerToIgnore2);
layerMask |= (1 << otherLayerToIgnore3);
//Invert to ignore it
layerMask = ~layerMask;	
//Convert Layer Name to Layer Number
int cubeLayerIndex = LayerMask.NameToLayer("cube");
int sphereLayerIndex = LayerMask.NameToLayer("sphere");

//Calculate layermask to Raycast to. (Ignore "cube" && "sphere" layers)
int layerMask = ~((1 << cubeLayerIndex) | (1 << sphereLayerIndex));