using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LibraryGame : MonoBehaviour
{

    public Transform bookMenu;
    public Transform bookSprites;
    public MonsterBook bookScript;

    // Start is called before the first frame update
    void Start()
    {
        bookMenu.gameObject.SetActive(false);
        bookScript = FindAnyObjectByType<MonsterBook>();

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

        while (rndNum.Count() < bookSprites.childCount / 2)
        {
            int rnd = Random.Range(0, bookSprites.childCount);
            if (!rndNum.Contains(rnd))
            {
                rndNum.Add(rnd);
            }
        }
        
        // then set sprite
        for(int i = 0; i < rndNum.Count; i++)
        {
            bookSprites.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("booksprite");
            bookSprites.GetChild(i).gameObject.SetActive(true);
            bookScript.idiomKey = i;
        }

    }
}
