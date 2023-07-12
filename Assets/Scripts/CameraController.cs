using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    float step = 100;
    public Button butt;
    // Start is called before the first frame update
    void Start()
    {
       // Button butt = gameObject.GetComponent<Button>();
        butt.onClick.AddListener(
            () =>
            {
                Camera.main.gameObject.transform.Translate(step, 0, 0);
            }
            );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
