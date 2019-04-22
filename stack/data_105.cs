using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class SPRITEBUTTON: MonoBehaviour, IPointerClickHandler,
                                  IPointerDownHandler, IPointerEnterHandler,
                                  IPointerUpHandler, IPointerExitHandler
{

    void Start()
    {
        //Attach Physics2DRaycaster to the Camera
        Camera.main.gameObject.AddComponent<Physics2DRaycaster>();

        addEventSystem();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Mouse Clicked!");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Mouse Down!");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Enter!");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Mouse Up!");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Exit!");
    }

    //Add Event System to the Camera
    void addEventSystem()
    {
        GameObject eventSystem = null;
        GameObject tempObj = GameObject.Find("EventSystem");
        if (tempObj == null)
        {
            eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }
        else
        {
            if ((tempObj.GetComponent<EventSystem>()) == null)
            {
                tempObj.AddComponent<EventSystem>();
            }

            if ((tempObj.GetComponent<StandaloneInputModule>()) == null)
            {
                tempObj.AddComponent<StandaloneInputModule>();
            }
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIButton : MonoBehaviour {

    public Sprite regular;
    public Sprite mouseOver;
    public Sprite mouseClicked;
    public TextMeshPro buttonText;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    private void OnMouseDown()
    {

    }

    private void OnMouseEnter()
    {

    }

    private void OnMouseExit()
    {

    }

    private void OnMouseUpAsButton()
    {

    }
}