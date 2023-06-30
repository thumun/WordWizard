using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookMenu : MonoBehaviour
{
    public Transform bookMenu;
    public Button exitBtn;

    public Transform choices; 
    public Button choiceOne;
    public Button choiceTwo;
    public Button choiceThree;

    public IdiomData data;

    // Start is called before the first frame update
    void Start()
    {
        MonsterChaosData.SetLoadFile("bookChaos");
        data = MonsterChaosData.GetIdioms();

        exitBtn.onClick.AddListener(exitMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void populateBook()
    {

        // randomize list
        List<bool> answers = new List<bool>();
        List<WrongIdiom> wrong = data.GetWrong();

        int rnd = Random.Range(0, 3);
        int count = 0; 

        for (int i = 0; i < 3; i++)
        {
            Transform child = choices.GetChild(i);

            //(rnd == i) ? answers[i] = true : answers[i] = true;
            if (rnd == i)
            {
                answers[i] = true;
                //child.gameObject.GetComponent<TextMeshProUGUI>().text = ;
                // choiceOne.GetComponent<TextMeshProUGUI>().text ;
            }
            else
            {
                answers[i] = false;
                child.gameObject.GetComponent<TextMeshProUGUI>().text = wrong[count].Idiom;
                count++; 
            }
        }
        
    }

    private void exitMenu()
    {
        bookMenu.gameObject.SetActive(false);
    }
}
