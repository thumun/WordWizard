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

    private Vector2 ScreenDimensions;

    // Start is called before the first frame update
    void Start()
    {
        canvasRect = (RectTransform)canvas.GetComponent<Transform>();
        ImageRect = (RectTransform)Image.GetComponent<Transform>();
        ImageRect.localScale = new Vector3((canvasRect.sizeDelta.x / ImageRect.sizeDelta.x) * xyRatios.x,
            (canvasRect.sizeDelta.y / ImageRect.sizeDelta.y) * xyRatios.y,
            ImageRect.localScale.z);

        ScreenDimensions = new Vector2(Screen.width, Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.width != ScreenDimensions.x || Screen.height != ScreenDimensions.y)
        {
            //ImageRect.localScale = new Vector3(canvasRect.sizeDelta.x / ImageRect.sizeDelta.x, canvasRect.sizeDelta.y / ImageRect.sizeDelta.y, ImageRect.localScale.z);
            ImageRect.localScale = new Vector3((canvasRect.sizeDelta.x / ImageRect.sizeDelta.x) * xyRatios.x,
                (canvasRect.sizeDelta.y / ImageRect.sizeDelta.y) * xyRatios.y,
                ImageRect.localScale.z);
        }
    }
}
