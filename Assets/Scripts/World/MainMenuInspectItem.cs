using UnityEngine;
using UnityEngine.EventSystems; // 1
using UnityEngine.UI;

public class MainMenuInspectItem : MonoBehaviour
    , IPointerClickHandler // 2
    , IDragHandler
    , IPointerEnterHandler
    , IPointerExitHandler
{

    public Transform objectToRot;
    

    public void OnPointerClick(PointerEventData eventData) // 3
    {
        print("I was clicked");
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("I'm being dragged!");
        Debug.Log(eventData.delta);
        objectToRot.Rotate(objectToRot.transform.up, eventData.delta.x);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}