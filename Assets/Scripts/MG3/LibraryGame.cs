using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LibraryGame : MonoBehaviour
{

    public Transform bookMenu;
    public Transform bookSprites;
    public Transform librarianTxt;

    // Start is called before the first frame update
    void Start()
    {
        bookMenu.gameObject.SetActive(false);
        bookSprites.gameObject.SetActive(false);
        BookSetUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BookSetUp()
    {
        // cycle through monsterbooks game obj and set some as sprites
        // to "randomize" location

        List<int> rndNum = new List<int>();

        for (int i = 0; i < bookSprites.childCount; i++)
        {
            bookSprites.GetChild(i).gameObject.SetActive(false);
        }

        // how to round up?
        while (rndNum.Count() < System.Math.Ceiling(bookSprites.childCount / 2.0))
        {
            // need to change this range to data 
            int rnd = Random.Range(0, bookSprites.childCount);
            if (!rndNum.Contains(rnd))
            {
                rndNum.Add(rnd);
            }
        }
        
        // then set sprite
        for(int i = 0; i < rndNum.Count; i++)
        {
            //bookSprites.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("booksprite");
            bookSprites.GetChild(rndNum[i]).gameObject.SetActive(true);
            bookSprites.GetChild(rndNum[i]).gameObject.GetComponent<MonsterBook>().idiomKey = rndNum[i];
        }

        bookSprites.gameObject.SetActive(true);

    }

    public void changeText(string change)
    {
        librarianTxt.gameObject.GetComponent<TextMeshProUGUI>().text = change; 
    }
}
