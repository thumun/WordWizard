using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookMenu : MonoBehaviour
{
    public Transform bookMenu;
    public Button exitBtn;
    public GameObject idiomImage;
    public GameObject passage; 

    public bool[] QuestionsBools = new bool[3];

    public Transform choices; 
    //public Button choiceOne;
    //public Button choiceTwo;
    //public Button choiceThree;

    public IdiomData data = new IdiomData();


    // Start is called before the first frame update
    void Start()
    {
        MonsterChaosData.SetLoadFile("bookChaos");
        data = MonsterChaosData.GetIdiomData();

        exitBtn.onClick.AddListener(exitMenu);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void populateBook(int spriteNum)
    {
        string spriteInfo = "";
        List<IdiomBase> answers = new List<IdiomBase>();
        answers = data.GetChoices(spriteNum);

        for (int i = 0; i < 3; i++)
        {
            Transform child = choices.GetChild(i);
            child.gameObject.GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = answers[i].Definition;
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

    private void responseClick()
    {

        bookMenu.gameObject.SetActive(false);
    }

    private void exitMenu()
    {
        bookMenu.gameObject.SetActive(false);
    }
}
