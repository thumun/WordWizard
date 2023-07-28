using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookMenu : MonoBehaviour
{
    public Transform currTxt;

    public Transform bookMenu;
    public Button exitBtn;
    public GameObject idiomImage;
    public GameObject passage; 

    public bool[] QuestionsBools = new bool[3];

    public Transform choices;
    public Button choiceOne;
    public Button choiceTwo;
    public Button choiceThree;

    public IdiomData data = new IdiomData();

    public LibraryGame libraryGameScript;

    GameObject currSprite; 

    // Start is called before the first frame update
    void Start()
    {
        MonsterChaosData.SetLoadFile("bookChaos");
        data = MonsterChaosData.GetIdiomData();

        libraryGameScript = FindAnyObjectByType<LibraryGame>();
        libraryGameScript.data = data;

        /*
        for (int i = 0; i < choices.childCount; i++)
        {
            Transform child = choices.GetChild(i);
            Button childbtn = child.gameObject.GetComponent<Button>();
            childbtn.onClick.AddListener(delegate { responseClick(i); });
        }
        */

        choiceOne.onClick.AddListener(delegate { responseClick(0); });
        choiceTwo.onClick.AddListener(delegate { responseClick(1); });
        choiceThree.onClick.AddListener(delegate { responseClick(2); });

        exitBtn.onClick.AddListener(exitMenu);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spriteData(int spriteNum, GameObject monster)
    {
        populateBook(spriteNum);
        currSprite = monster;
    }

    public void populateBook(int spriteNum)
    {
        string spriteInfo = "";
        List<IdiomBase> answers = new List<IdiomBase>();
        answers = data.GetChoices(spriteNum);

        for (int i = 0; i < 3; i++)
        {
            Transform child = choices.GetChild(i);
            Button childbtn = child.gameObject.GetComponent<Button>();
            childbtn.GetComponentInChildren<TextMeshProUGUI>().text = answers[i].Definition;

            QuestionsBools[i] = answers[i].IsCorrect;

            if (answers[i].IsCorrect)
            {
                spriteInfo = answers[i].Idiom; // if sprite name same as idiom
                passage.GetComponent<TextMeshProUGUI>().text = answers[i].Example;
            }
        }
        // set sprite here 
        idiomImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(spriteInfo);
    }

    public void responseClick(int name)
    {
        int correctIndx = -1;
        for (int i = 0; i < QuestionsBools.Length; i++)
        {
            if (QuestionsBools[i] == true)
            {
                correctIndx = i;
                break;
            }
        }

        if (correctIndx == -1)
        {
            Debug.Log("Something went wrong with response -> check bools");
        }
        else
        {
            if (name == correctIndx)
            {
                Debug.Log("Right answer!");

                libraryGameScript.librarianTxt.gameObject.GetComponent<TextMeshProUGUI>().text =
                    libraryGameScript.dialogue.getGoodFeedback();

                libraryGameScript.timePassed = 0f;

                // wait ??
                //StartCoroutine(WaitTime());
                currTxt.gameObject.GetComponent<TextMeshProUGUI>().text = (int.Parse(currTxt.gameObject.GetComponent<TextMeshProUGUI>().text) + 1).ToString();
                currSprite.SetActive(false);
                bookMenu.gameObject.SetActive(false);

            }
            else
            {
                Debug.Log("Wrong answer!");
                // librarian negative dialogue
                libraryGameScript.librarianTxt.gameObject.GetComponent<TextMeshProUGUI>().text =
                    libraryGameScript.dialogue.getBadFeedback();
            }
        }
    }

    private void exitMenu()
    {
        bookMenu.gameObject.SetActive(false);
    }

    public void resetScore()
    {
        currTxt.gameObject.GetComponent<TextMeshProUGUI>().text = "0";
    }

    /*
    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(10);
    }
    */
}
