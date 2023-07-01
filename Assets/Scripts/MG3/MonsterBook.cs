using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NReco.Csv;
using System.IO;
using System.Linq;

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
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        // lib book clicked

        //bookMenu.gameObject.GetComponent<BookMenu>().populateBook(idiomKey);

        bookMenuScript.populateBook(idiomKey);
        bookMenu.gameObject.SetActive(true);
        //BookMenu.populateBook();
    }

    void OnMouseOver()
    {
        // highlight ? 
        
    }

    




}
