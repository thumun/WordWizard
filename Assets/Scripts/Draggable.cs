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
    private GameObject target;

    RectTransform targetRect;


    [SerializeField]
    private GameObject wordBox;

    public GameObject anchorCur;
    public GameObject anchorOrigin;
    public bool canMove;
    public bool overTarget;

    private Vector2 resolution;

    private void Start()
    {
        targetRect = (RectTransform)target.transform;
        resolution = new Vector2(Screen.width, Screen.height);
        canMove = true;
    }
    public static Rect RectTransformToScreenSpace(RectTransform transform)
    {
        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        return new Rect((Vector2)transform.position - (size * 0.5f), size);
    }

    public void setAnchorCur(GameObject anchor)
    {
        anchorCur = anchor;
    }

    public void setAnchorOrigin(GameObject anchor)
    {
        anchorOrigin = anchor;
    }

    public void DragHandler(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerData.position,
            canvas.worldCamera,
            out position);
        if (canMove)
        {
            transform.position = canvas.transform.TransformPoint(position);
        }
    }

    public void MouseUp()
    {
        Rect screenTargetRect = RectTransformToScreenSpace(targetRect);


        if (Input.mousePosition.y < (screenTargetRect.y + screenTargetRect.height)
            && Input.mousePosition.y > (screenTargetRect.y))
        { 
            overTarget = true;
            Debug.Log("Mouse is in answer box");
        }
        transform.position = anchorCur.transform.position;
    }

    private void Update()
    {
        if (resolution.x != Screen.width || resolution.y != Screen.height)
        {

            transform.position = anchorCur.transform.position;

            resolution.x = Screen.width;
            resolution.y = Screen.height;
        }
    }
}
