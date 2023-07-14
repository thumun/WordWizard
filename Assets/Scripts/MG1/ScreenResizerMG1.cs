using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResizerMG1 : MonoBehaviour
{
    [SerializeField]
    public GameObject content;
    private RectTransform contentRect;

    // Start is called before the first frame update
    void Start()
    {
        contentRect = (RectTransform)content.GetComponent<Transform>();
        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Screen.width / Screen.height;
        content.transform.localScale = new Vector3(width / contentRect.rect.width,
            content.transform.localScale.y,
            content.transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Screen.width / Screen.height;
        content.transform.localScale = new Vector3(width / contentRect.rect.width,
            content.transform.localScale.y,
            content.transform.localScale.z);
    }
}
