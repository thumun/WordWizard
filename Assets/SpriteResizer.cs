using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteResizer : MonoBehaviour
{


    private RectTransform ImageRect;
    private RectTransform CanvasRect;

    [SerializeField]
    public GameObject Image;

    [SerializeField]
    public GameObject Canvas;

    // Start is called before the first frame update
    void Start()
    {
        ImageRect = (RectTransform)Image.GetComponent<Transform>();
        CanvasRect = (RectTransform)Canvas.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanvasRect.sizeDelta != ImageRect.sizeDelta)
        {
            ImageRect.localScale = new Vector2(CanvasRect.sizeDelta.x / ImageRect.sizeDelta.x,
                CanvasRect.sizeDelta.y / ImageRect.sizeDelta.y);
        }
    }
}
