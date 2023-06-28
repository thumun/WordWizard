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

    static bool test = true;

    ListeningDialogue listeningDialogue;


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
        listeningDialogue.setup(1);


    }

    void noBtnLogic()
    {
        confirmation.gameObject.SetActive(false);
        test = true; 
    }

    void OnMouseDown()
    {
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
