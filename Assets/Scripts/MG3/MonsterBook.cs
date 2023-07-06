using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NReco.Csv;
using System.IO;
using System.Linq;
using System;

public class MonsterBook : MonoBehaviour
{
    public Transform bookMenu;
    public int idiomKey;
    public BookMenu bookMenuScript;

    // Start is called before the first frame update
    void Start()
    {
        bookMenuScript = FindAnyObjectByType<BookMenu>();

    }

    // Update is called once per frame
    float timePassed = 0f;
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > 100f)
        {
            
        }
    }

    void OnMouseDown()
    {
        // lib book clicked
        bookMenuScript.spriteData(idiomKey, this.gameObject);
        //bookMenuScript.populateBook(idiomKey);
        bookMenu.gameObject.SetActive(true);
        //this.gameObject.SetActive(false);
    }

    void OnMouseOver()
    {
        // highlight ? 

    }

  
}

  
