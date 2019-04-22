public class StandaloneInputModuleV2 : StandaloneInputModule
{
    public GameObject GameObjectUnderPointer(int pointerId)
    {
        var lastPointer = GetLastPointerEventData(pointerId);
        if (lastPointer != null)
            return lastPointer.pointerCurrentRaycast.gameObject;
        return null;
    }

    public GameObject GameObjectUnderPointer()
    {
        return GameObjectUnderPointer(PointerInputModule.kMouseLeftId);
    }
}
private static StandaloneInputModuleV2 currentInput;
private StandaloneInputModuleV2 CurrentInput
{
    get
    {
        if (currentInput == null)
        {
            currentInput = EventSystem.current.currentInputModule as StandaloneInputModuleV2;
            if (currentInput == null)
            {
                Debug.LogError("Missing StandaloneInputModuleV2.");
                // some error handling
            }
        }

        return currentInput;
    }
}

 if(Input.GetMouseButton(0))
 {
     PointerEventData pointer = new PointerEventData(EventSystem.current);
     pointer.position = Input.mousePosition;

     List<RaycastResult> raycastResults = new List<RaycastResult>();
     EventSystem.current.RaycastAll(pointer, raycastResults);

     if(raycastResults.Count > 0)
     {
         foreach(var go in raycastResults)
         {  
             Debug.Log(go.gameObject.name,go.gameObject);
         }
     }
 }
 using UnityEngine.EventSystems;
public class MouseEnterScript: MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Name: " + eventData.pointerCurrentRaycast.gameObject.name);
    }
}