using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MinigameTwo
{
    public class AnswerBlank : MonoBehaviour
    {

        public QuestionFormats curQues;

        public void setCurQues(QuestionFormats cQ)
        {
            curQues = cQ;
        }

        [SerializeField]
        public GameObject AnswerMenu;

        [SerializeField]
        public GameObject OopsMenu;

        [SerializeField]
        public GameObject inputField;

        [SerializeField]
        public GameObject content;

        public void onSubmitClick()
        {
            string answer = inputField.GetComponent<TMP_InputField>().text;
            Debug.Log(curQues.correctAnswer);

            if (curQues.correctAnswer.Equals(answer))
            {
                string text = content.GetComponent<TMP_Text>().text;

                text = text.Remove((curQues.indexWithTags), curQues.word.Length);
                text = text.Insert((curQues.indexWithTags), answer);

                text = text.Remove((curQues.indexWithTags - 2 - curQues.word.Length), curQues.word.Length);
                text = text.Insert((curQues.indexWithTags - 2 - curQues.word.Length), answer);

                content.GetComponent<TMP_Text>().SetText(text);
            }
            else
            {
                OopsMenu.SetActive(true);
            }

            AnswerMenu.SetActive(false);
            inputField.GetComponent<TMP_InputField>().SetTextWithoutNotify("");
            curQues = new QuestionFormats();
        }
    }
}