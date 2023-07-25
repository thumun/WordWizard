using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MinigameTwo
{
    public class Minigame2Manager : MonoBehaviour
    {
        public List<QuestionFormats> clickableWords;

        public string formatText;

        public int TODO;
        public int DONE;

        private string inputString;

        [SerializeField]
        public GameObject managerMenu;

        [SerializeField]
        public GameObject content;

        [SerializeField]
        public GameObject inputField;

        [SerializeField]
        public GameObject scoreBox;

        [SerializeField]
        public GameObject winScreen;

        private bool winAppear;

        // Start is called before the first frame update
        void Start()
        {
            string[] passages = new string[] { "MG2Passage1", "MG2Passage2" };
            int randNum = UnityEngine.Random.Range(0, passages.Length);
            string filename = passages[randNum];
            filename = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Assets", "Data", filename + ".txt");

            inputString = File.ReadAllText(filename);

            winAppear = false;
            TODO = 0;
            DONE = 0;

            // This will be where the list/string reader will be
            // the final result will be a formatted string with link tags representing each word.
            // Repeat words will share the same tag.

            string[] pWords = inputString.Split("#");
            string[] tempArray;
            List<string> words = new List<string>();

            bool mistake = false;

            foreach (string s in pWords)
            {
                if (!mistake)
                {
                    tempArray = s.Split(" ");
                    foreach (string t in tempArray)
                    {
                        if (!t.Equals(""))
                        {
                            words.Add(t);
                        }
                    }
                }
                else
                {
                    words.Add("#" + s);
                }
                mistake = !mistake;
            }

            mistake = false;

            QuestionFormats tempQF;

            int keyIndex = 0;
            string tempStr;

            for (int i = 0; i < words.Count; i++)
            {
                tempStr = words[i];
                if (tempStr[0].Equals('#'))
                {
                    tempStr = tempStr.Remove(0, 1);
                    mistake = true;
                }
                tempQF = new QuestionFormats();

                tempQF.Keyword = keyIndex.ToString();
                tempQF.mistake = mistake;
                if (tempQF.mistake)
                {
                    tempQF.word = tempStr.Remove(tempStr.IndexOf("$"));
                    tempQF.correctAnswer = tempStr.Remove(0, tempStr.IndexOf("$") + 1);
                    TODO++;
                }
                else
                {
                    tempQF.word = tempStr;
                    tempQF.correctAnswer = tempStr;
                }
                tempQF.Keyword = tempQF.word;

                // Getting Index with tags

                List<string> soFarList = words.GetRange(0, i);
                string soFar = "";

                foreach (string s in soFarList)
                {
                    soFar += s + " ";
                }

                string soFarNoSpace = soFar.Replace(" ", "");

                tempQF.indexWithTags = 9 + (16 * i) + (soFar.Length + soFarNoSpace.Length) + tempQF.word.Length;

                clickableWords.Add(tempQF);
                keyIndex++;
                mistake = false;
            }

            formatText = "";

            foreach (QuestionFormats q in clickableWords)
            {
                formatText += "<link=" + "\"" + q.Keyword + "\">" + q.word + "</link>" + " ";
                managerMenu.GetComponent<TooltipHandler>().AddtoQuestions(q);
            }

            if (clickableWords.Count != 0)
            {
                formatText.Remove(formatText.Length - 1);
            }

            content.GetComponent<TMP_Text>().text = formatText;

        }

        // Update is called once per frame
        void Update()
        {
            if (DONE == TODO && winAppear == false)
            {
                winScreen.SetActive(true);
                winAppear = true;
            }

            scoreBox.GetComponent<TMP_Text>().SetText(DONE + "/" + TODO);
        }
    }
}
