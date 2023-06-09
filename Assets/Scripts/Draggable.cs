using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Vector2 pos;

    [SerializeField]
    private GameObject wordBox;

    public void DragHandler(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerData.position,
            canvas.worldCamera,
            out position);
        
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void OnClick()
    {
        //GameObject dupe = GameObject.Instantiate(wordBox);
        //dupe.transform.position = new Vector3(2.0f, 2.0f, 2.0f);
    }
}
