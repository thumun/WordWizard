using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHorizontalCanvasResizer : MonoBehaviour
{
    public Vector2 xyRatios;

    public GameObject canvas;

    private RectTransform canvasRect;

    public GameObject Image;

    private RectTransform ImageRect;

    // Start is called before the first frame update
    void Start()
    {
        canvasRect = (RectTransform)canvas.GetComponent<Transform>();
        ImageRect = (RectTransform)Image.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //ImageRect.localScale = new Vector3(canvasRect.sizeDelta.x / ImageRect.sizeDelta.x, canvasRect.sizeDelta.y / ImageRect.sizeDelta.y, ImageRect.localScale.z);
        ImageRect.localScale = new Vector3((canvasRect.sizeDelta.x / ImageRect.sizeDelta.x) * xyRatios.x,
            (canvasRect.sizeDelta.y / ImageRect.sizeDelta.y) * xyRatios.y,
            ImageRect.localScale.z);
    }
}
