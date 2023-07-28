using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class spriteoptions : MonoBehaviour
{
    public static bool clicking;

    //public Transform confirmation;
    public Transform tenseInfo;
    public GameObject tenseText;
    public GameObject tenseTitle;

    public string tense;
    public string tenseData;
    public Button yes;
    public Button no;

    public Transform oppSprite;
    public Sprite[] sprites;

    //public static bool disableMouseOver = true;

    ListeningDialogue listeningDialogue;
    //int indx = -1;


    // Start is called before the first frame update
    void Start()
    {
        clicking = false;
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
        tenseInfo.gameObject.SetActive(false);
        listeningDialogue.setup();
    }

    void noBtnLogic()
    {
        tenseInfo.gameObject.SetActive(false);
        this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(System.IO.Path.Combine("WDMiniMenu", this.name+" Bkg"));
        //disableMouseOver = true;
        listeningDialogue.indx = -1;
    }

    void OnMouseDown()
    {
        if (!clicking)
        {
            clicking = true;
            //Transform tenseChild = tenseInfo.transform.GetChild(1);
            tenseTitle.GetComponent<TextMeshProUGUI>().text = tense;
            //tenseChild = tenseInfo.transform.GetChild(2);
            tenseText.GetComponent<TextMeshProUGUI>().text = tenseData;

            tenseInfo.transform.gameObject.SetActive(true);

            //Transform child = confirmation.GetChild(2);
            //child.GetComponent<TextMeshProUGUI>().text = tense;
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
                Sprite curSprite = sprites[listeningDialogue.indx];

                // adding highlight to sprite in menu
                this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(System.IO.Path.Combine("WDOutlines", curSprite.name + "BkgOutline"));

                // setting opp sprite in game
                string curPath = System.IO.Path.Combine("WizardBattle", curSprite.name + "Neutral");
                Debug.Log(curPath);
                oppSprite.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(curPath);
                listeningDialogue.tense = tense;

            }

            //confirmation.gameObject.SetActive(true);
            // disable move over ?? 
            //disableMouseOver = false;
        }
    }

    public static void setClicking(bool set)
    {
        clicking = set;
    }

    /*
    void OnMouseOver()
    {
        if (disableMouseOver)
        {
            Transform child = tenseInfo.transform.GetChild(1);
            child.gameObject.GetComponent<TextMeshProUGUI>().text = tense;
            child = tenseInfo.transform.GetChild(2);
            child.gameObject.GetComponent<TextMeshProUGUI>().text = tenseData;

            tenseInfo.transform.gameObject.SetActive(true);
            //test = false;
        }
    }
    */

    /*
    void OnMouseExit()
    {
        tenseInfo.transform.gameObject.SetActive(false);
    }
    */
}
