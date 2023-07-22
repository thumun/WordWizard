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

    public List<string> modesTemp; 
}

public class DuelData
{
    public string opponent; 
    public List<ResponseChoice> attack;

    public DuelData()
    {

    }

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

    public GameObject oppSprite;
    public GameObject yourSprite;
    public Sprite[] sprites;

    public GameObject tenseInfo; 

    public string category; 

    public GameObject gameScreen;
    public GameObject gameScreenUI;
    public GameObject miniMenuWD;
    public GameObject minimenuUI;

    private List<DuelData> spells;
    private bool correctanswer = false;
    private List<Dialogue> dialogue;
    private int dialogueNum = 0;
    private int initialRounds;

    public int indx = -1;
    public string tense = "";

    public WizardData duelInfo;

    // Start is called before the first frame update
    void Start()
    {
        WizardDuelData.SetLoadFile("wizardDuel");
        duelInfo = WizardDuelData.GetWizardData();

        initialRounds = rounds;
        win.gameObject.SetActive(false);
        lose.gameObject.SetActive(false);

        one.onClick.AddListener(delegate { responseClick(0); });
        two.onClick.AddListener(delegate { responseClick(1); });
        three.onClick.AddListener(delegate { responseClick(2); });

        //responses.gameObject.SetActive(false);

        // putting the file read here initially but should be else where...
        //dialogue = ReadSpells("miniGame1");

        //updateResponseBtns();
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
            rounds--;

        } else if (rounds > 0)
        {
            if (correctanswer)
            {
                // when round finished -> go to next round
                rounds -= 1;

                if (rounds > 0)
                {
                    dialogueNum += 1;

                    updateResponseBtns();
                    responses.gameObject.SetActive(true);

                    correctanswer = false;
                }
                
            }
        } else
        {
            //Debug.Log(dialogueNum);
        }
    }

    public void setup()
    {
        //oppSprite.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[indx];
        gameScreen.SetActive(true);
        gameScreenUI.SetActive(true);
        spells = createDialogue(initialRounds, category);
        updateResponseBtns();
        tenseInfo.GetComponent<TextMeshProUGUI>().text = tense;
        miniMenuWD.SetActive(false);
        minimenuUI.SetActive(false);
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
                StartCoroutine(HurtAnim(oppSprite));
               
            }
            else
            {
                loseLife();
                Debug.Log("Wrong answer!");
                StartCoroutine(PlayerHurtAnim(yourSprite));

            }
        }
    }

    public void updateResponseBtns()
    {

        one.GetComponentInChildren<TextMeshProUGUI>().text = spells[dialogueNum].attack[0].Choice.Response;
        two.GetComponentInChildren<TextMeshProUGUI>().text = spells[dialogueNum].attack[1].Choice.Response;
        three.GetComponentInChildren<TextMeshProUGUI>().text = spells[dialogueNum].attack[2].Choice.Response;

        QuestionsBools[0] = spells[dialogueNum].attack[0].IsCorrect;
        QuestionsBools[1] = spells[dialogueNum].attack[1].IsCorrect;
        QuestionsBools[2] = spells[dialogueNum].attack[2].IsCorrect;

        oppDialogue.GetComponent<TextMeshProUGUI>().text = spells[dialogueNum].opponent;

        /*
        one.GetComponentInChildren<TextMeshProUGUI>().text = dialogue[dialogueNum].attack[0];
        two.GetComponentInChildren<TextMeshProUGUI>().text = dialogue[dialogueNum].attack[1];
        three.GetComponentInChildren<TextMeshProUGUI>().text = dialogue[dialogueNum].attack[2];

        QuestionsBools[0] = dialogue[dialogueNum].attCorrect[0] == 1;
        QuestionsBools[1] = dialogue[dialogueNum].attCorrect[1] == 1;
        QuestionsBools[2] = dialogue[dialogueNum].attCorrect[2] == 1;

        oppDialogue.GetComponent<TextMeshProUGUI>().text = dialogue[dialogueNum].opponent;
        */

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
            // resetgame
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
        ResetGame();
    }

    // https://www.youtube.com/watch?v=oNz4I0RfsEg 
    IEnumerator FadeTransition(Transform bg)
    {
        Transform child = bg.GetChild(0);
        Color c = child.GetComponent<Image>().color;
        c.a = 0f;
        child.GetComponent<Image>().color = c;

        bg.gameObject.SetActive(true);

        for (float f = 0.05f; f <1.05; f += 0.05f)
        {
            c = bg.GetChild(0).GetComponent<Image>().color;
            c.a = f;
            bg.GetChild(0).GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator HurtAnim(GameObject character)
    {
        Sprite origSprite = character.GetComponent<SpriteRenderer>().sprite;
        String origName = origSprite.name.Replace("Neutral", "");
        String hurtPath = System.IO.Path.Combine("WizardBattle", origName + "Attacked");
        Debug.Log(hurtPath);
        Sprite hurt = Resources.Load<Sprite>(hurtPath);

        for (float f = 0.0f; f < 0.9; f += 0.3f)
        {
            character.GetComponent<SpriteRenderer>().sprite = hurt;
            yield return new WaitForSeconds(0.15f);
            character.GetComponent<SpriteRenderer>().sprite = origSprite;
            yield return new WaitForSeconds(0.15f);
        }
    }

    IEnumerator PlayerHurtAnim(GameObject character)
    {
        Color c = yourSprite.GetComponent<SpriteRenderer>().color;
        Color r = new Color(1.0f, 0.0f, 0.0f);

        for (float f = 0.0f; f < 0.9; f += 0.3f)
        {
            yourSprite.GetComponent<SpriteRenderer>().color = r;
            yield return new WaitForSeconds(0.15f);
            yourSprite.GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(0.15f);
        }
    }

    public void ResetGame()
    {
        // reseting vals used in mini menu
        indx = -1;
        tense = "";

        spriteoptions.disableMouseOver = true;

        //gameScreen.SetActive(false);
        //gameScreenUI.SetActive(false);
        //miniMenuWD.SetActive(true);
        //minimenuUI.SetActive(true);

        //yield return new WaitForSeconds(0.05f);
    }

    public List<DuelData> createDialogue(int round, string categoryName)
    {
        List<DuelData> spells = new List<DuelData>();

        // need to cycle through duel data and get specific category
        Category category = duelInfo.Categories.Where(c => c.Name == categoryName).First();

        // then get questions from list 
        Question.ShuffleMe(category.Questions); // randomize the questions
        
        for (int j = 0; j < round; j++)
        {
            DuelData spell = new DuelData();
            spell.opponent = category.Questions[j].Ask;
            spell.attack = category.Questions[j].PresentChoices(tense);
            spells.Add(spell);
        }

        // adding rand for ending
        DuelData filler = new DuelData();
        filler.opponent = "Opponent info here";

        List<ResponseChoice> fillerAtk = new List<ResponseChoice>();
        fillerAtk.Add(new ResponseChoice(new Choice("test", "test"), false));
        fillerAtk.Add(new ResponseChoice(new Choice("test", "test"), false));
        fillerAtk.Add(new ResponseChoice(new Choice("test", "test"), false));

        filler.attack = fillerAtk;

        return spells; 
    }

    /*
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
    */
}
