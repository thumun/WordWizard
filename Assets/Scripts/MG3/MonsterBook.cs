using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBook : MonoBehaviour
{
    public Transform bookMenu;
    public string idiomKey; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        // lib book clicked
        bookMenu.gameObject.SetActive(true);
        //BookMenu.populateBook();
    }

    void OnMouseOver()
    {
        // highlight ? 
        
    }

    

    
}
