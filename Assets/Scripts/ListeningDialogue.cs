using System.Collections;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Net;
using System;
using NReco.Csv;
using System.IO;
using UnityEngine.SceneManagement;


public class Dialogue
{
    public string category;
    public string name;
    public string opponent;
    public List<string> attack = new List<string>();
    public List<int> attCorrect = new List<int>();
    //public List<string> defense = new List<string>();
    //public List<int> defCorrect = new List<int>();

    /*
    int numQuestions;
    int numAnswers;
    List<Questions> convos;

    public class Questions
    {
        List<(string, bool)> answers;

        public Questions(int numAnswers)
        {
            for (int i = 0; i < numAnswers; i++)
            {

            }
        }
    }
    */

}


public class ListeningDialogue : MonoBehaviour
{

    public int rounds;

    public Transform responses;

    // will change this based on how we read in data 
    //public string[] Questions = new string[3];
    public bool[] QuestionsBools = new bool[3];

    //public GameObject roundObj;
    public GameObject oppDialogue;

    public GameObject oppInfo;
    public Transform lives;

    public Transform lose;
    public Transform win;

    public Button one;
    public Button two;
    public Button three;

    private bool correctanswer = false;
    private List<Dialogue> dialogue;
    private int dialogueNum = 0;
    private int initialRounds;

    // Start is called before the first frame update
    void Start()
    {
        initialRounds = rounds;
        win.gameObject.SetActive(false);
        lose.gameObject.SetActive(false);

        one.onClick.AddListener(delegate { responseClick(0); });
        two.onClick.AddListener(delegate { responseClick(1); });
        three.onClick.AddListener(delegate { responseClick(2); });

        responses.gameObject.SetActive(false);

        // putting the file read here initially but should be else where...
        dialogue = ReadSpells("miniGame1");

        updateResponseBtns();
        responses.gameObject.SetActive(true);

        endScreen(win);
        endScreen(lose);

    }

    // Update is called once per frame
    void Update()
    {
        //roundObj.GetComponent<TextMeshProUGUI>().text = rounds.ToString();
        if (rounds == 0)
        {
            gameOver("w");

        } else
        {
            if (correctanswer)
            {
                // when round finished -> go to next round

                dialogueNum += 1;
                updateResponseBtns();
                responses.gameObject.SetActive(true);

                correctanswer = false;
                rounds -= 1;
            }
        }
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
                correctanswer = true;
                responses.gameObject.SetActive(false);
               
            }
            else
            {
                loseLife();
                Debug.Log("Wrong answer!");

            }
        }
    }

    public void updateResponseBtns()
    {
        // should get new content from data & update
        // question strings && bool based on data

        one.GetComponentInChildren<TextMeshProUGUI>().text = dialogue[dialogueNum].attack[0];
        two.GetComponentInChildren<TextMeshProUGUI>().text = dialogue[dialogueNum].attack[1];
        three.GetComponentInChildren<TextMeshProUGUI>().text = dialogue[dialogueNum].attack[2];

        QuestionsBools[0] = dialogue[dialogueNum].attCorrect[0] == 1;
        QuestionsBools[1] = dialogue[dialogueNum].attCorrect[1] == 1;
        QuestionsBools[2] = dialogue[dialogueNum].attCorrect[2] == 1;

        oppDialogue.GetComponent<TextMeshProUGUI>().text = dialogue[dialogueNum].opponent;

    }

    public void gameOver(string end)
    {
        //responses.gameObject.SetActive(false);
        //oppDialogue.SetActive(false);
        Debug.Log("Game over!");

        if (end == "w")
        {
            Debug.Log("Win");
            StartCoroutine(FadeTransition(win));
        } else
        {
            // if we want to add a thing for more hearts --> add here
            // as extra elif 
            Debug.Log("Loss/Quit");
            StartCoroutine(FadeTransition(lose));
        }
    }

    public void loseLife()
    {
        // losing hearts logic   

        for (int index = 0; index < lives.childCount; index++)
        {
            Transform child = lives.GetChild(index);

            if(child.gameObject.GetComponent<Image>().color != Color.black)
            {
                child.gameObject.GetComponent<Image>().color = Color.black;
                break;
            }

            if (index == lives.childCount - 1)
            {
                gameOver("l");
            }
        }
           
        // sprite getting hit

    }

    public void endScreen(Transform bg)
    {
        if (bg.name == "GameOver")
        {
            Button retry = bg.GetChild(2).GetComponent<Button>();
            retry.onClick.AddListener(retryBtn);
        }

        Button quit = bg.GetChild(3).GetComponent<Button>();
        quit.onClick.AddListener(quitBtn);
        
    }

    // would retry have the same questions?
    private void retryBtn()
    {
        // figure out restart
        dialogueNum = 0; 
        updateResponseBtns();
        for (int index = 0; index < lives.childCount; index++)
        {
            Transform child = lives.GetChild(index);
            child.gameObject.GetComponent<Image>().color = Color.white;
        }

        lose.gameObject.SetActive(false);
        rounds = initialRounds;
    }

    private void quitBtn()
    {
        // perhaps should quit back to the inside of building later 
        SceneManager.LoadScene("MainMenu");
    }

    // kinda grey'd out??
    // https://www.youtube.com/watch?v=oNz4I0RfsEg 
    IEnumerator FadeTransition(Transform bg)
    {
        Transform child = bg.GetChild(0);
        Color c = child.GetComponent<Image>().color;
        c.a = 0f;
        child.GetComponent<Image>().color = c;

        bg.gameObject.SetActive(true);

        for (float f = 0.05f; f <=1; f += 0.05f)
        {
            c = child.GetComponent<Image>().color;
            c.a = f;
            child.GetComponent<Image>().color = c;

        }
        yield return new WaitForSeconds(0.05f);
    }

    public static List<Dialogue> ReadSpells(string fileName)
    {
        List<Dialogue> spells = new List<Dialogue>();

        using (var streamRdr = new StreamReader(System.IO.Directory.GetCurrentDirectory() + @"/Assets/Data/" + fileName + ".csv"))
        {
            var csvReader = new CsvReader(streamRdr, ",");
            csvReader.Read();
            while (csvReader.Read())
            {

                int j = 0;

                List<string> temp = new List<string>();

                Debug.Log(csvReader[j]);

                if (csvReader[j] == "Adjective")
                {
                    Dialogue spell = new Dialogue();

                    spell.name = csvReader[j + 1];
                    spell.opponent = csvReader[j + 2];

                    temp = csvReader[j + 3].Trim('[', ']').Split(',').ToList();

                    for (int i = 0; i < temp.Capacity; i++)
                    {
                        spell.attack.Add(temp[i]);
                    }

                    temp = csvReader[j + 4].Trim('[', ']').Split(',').ToList();
                    for (int i = 0; i < temp.Capacity; i++)
                    {
                        spell.attCorrect.Add(Convert.ToInt32(temp[i]));
                    }

                    spells.Add(spell);
                }
            }
        }
        return spells;
    }

}
