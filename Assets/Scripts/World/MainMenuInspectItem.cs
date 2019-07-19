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
    public float scrollMult;

    public void OnPointerClick(PointerEventData eventData) // 3
    {
        print("I was clicked");
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("I'm being dragged!");
        Debug.Log(eventData.delta);
        objectToRot.transform.GetChild(0).Rotate(objectToRot.transform.up, eventData.delta.x);
        float scroll = Input.mouseScrollDelta.y;
        objectToRot.position += new Vector3(0, 0, scroll * scrollMult * Time.deltaTime);



    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}