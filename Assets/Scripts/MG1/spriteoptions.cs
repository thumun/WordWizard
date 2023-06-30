using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class spriteoptions : MonoBehaviour
{

    public Transform confirmation;
    public Transform tenseInfo;
    public string tense;
    public string tenseData;
    public Button yes;
    public Button no;

    public Transform oppSprite;
    public Sprite[] sprites;

    static bool test = true;

    ListeningDialogue listeningDialogue;
    //int indx = -1;


    // Start is called before the first frame update
    void Start()
    {
        // yesBtn
        //confirmation.GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(startGame);
        yes.onClick.AddListener(startGame);
        // nBtn
        //confirmation.GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(noBtnLogic);
        no.onClick.AddListener(noBtnLogic);

        listeningDialogue = FindAnyObjectByType<ListeningDialogue>();

    }

    void startGame()
    {
        // start game

        /*
        if(tense == "Present Continuous")
        {
            indx = 0;
        } else if (tense == "Present Perfect")
        {
            indx = 1;
        }
        else if (tense == "Present Perfect Continuous")
        {
            indx = 2;
        }
        else if (tense == "Past Continuous")
        {
            indx = 3;
        }
        else if (tense == "Past Perfect Continuous")
        {
            indx = 4;
        }
        else if (tense == "Future Simple")
        {
            indx = 5;
        }
        */

        listeningDialogue.setup();
    }

    void noBtnLogic()
    {
        confirmation.gameObject.SetActive(false);
        test = true;
        listeningDialogue.indx = -1;
    }

    void OnMouseDown()
    {
        Transform child = confirmation.GetChild(2);
        child.GetComponent<TextMeshProUGUI>().text = tense;
        if (listeningDialogue.indx == -1)
        {
            if (tense == "Present Continuous")
            {
                listeningDialogue.indx = 0;
            }
            else if (tense == "Present Perfect")
            {
                listeningDialogue.indx = 1;
            }
            else if (tense == "Present Perfect Continuous")
            {
                listeningDialogue.indx = 2;
            }
            else if (tense == "Past Continuous")
            {
                listeningDialogue.indx = 3;
            }
            else if (tense == "Past Perfect Continuous")
            {
                listeningDialogue.indx = 4;
            }
            else if (tense == "Future Simple")
            {
                listeningDialogue.indx = 5;
            }
            oppSprite.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[listeningDialogue.indx];
            listeningDialogue.tense = tense;
        }
        
        confirmation.gameObject.SetActive(true);
        // disable move over ?? 
        test = false;
    }

    void OnMouseOver()
    {
        if (test)
        {
            Transform child = tenseInfo.GetChild(1);
            child.gameObject.GetComponent<TextMeshProUGUI>().text = tense;
            child = tenseInfo.GetChild(2);
            child.gameObject.GetComponent<TextMeshProUGUI>().text = tenseData;

            tenseInfo.gameObject.SetActive(true);
            //test = false;
        }
    }

    void OnMouseExit()
    {
        tenseInfo.gameObject.SetActive(false);
    }
}
