using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WDMenu : MonoBehaviour
{

    public GameObject cat;
    public GameObject pig;
    public GameObject rabbit;
    public GameObject deer;
    public GameObject temp1;
    public GameObject temp2; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] results = new RaycastHit2D[6];

        var result = Physics2D.GetRayIntersection(ray, 100.0f, 0);

        
       
    }


}
