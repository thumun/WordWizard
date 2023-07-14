using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMover : MonoBehaviour
{

    [SerializeField]
    public GameObject BG;

    [SerializeField]
    public GameObject mainCamera;

    [SerializeField]
    public float zOffset;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        BG.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z + zOffset);
        
    }
}
