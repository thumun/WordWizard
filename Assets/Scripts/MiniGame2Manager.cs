using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

// TODO: Move the selector movement to this script, then modify inputText to censor blanks
// Then, add anchors, then draggables

public class MiniGame2Manager : MonoBehaviour
{
    // This is the input for the puzzle, can either be list of strings or string
    [SerializeField]
    string inputString;
    [SerializeField]
    string[] inputList;

    // The box of readable text
    [SerializeField]
    public GameObject textBox;
    // List fo words that gets added to the textbox
    public List<wordText> words;

    // The object for marking text
    [SerializeField]
    GameObject Selector;

    public struct wordText
    {
        // Important for blanks
        public GameObject anchor;

        // Universally needed values
        public string word;
        public int charIndex;
        public int length;
        public bool blank;
        public RectTransform rect;
        public Vector3 pos;
    };

    // Start is called before the first frame update
    void Start()
    {


        if ((inputString != null || !inputString.Equals("")) && (inputList.Length == 0 || inputList == null))
        {
            inputList = inputString.Split(" ");
        }
        else if ((inputList.Length != 0 || inputList != null) && (inputString.Equals("") || inputString == null))
        {
            inputString = "";
            foreach (string s in inputList)
            {
                inputString = s + " ";
            }
            inputString = inputString.Remove(inputString.Length - 1);
        }
        else
        {
            Debug.Log("WARNING: No text has been set for MiniGame2");
        }

        wordText tempWordText;
        List<Vector3> tempCorners;
        int index = 0;
        foreach (string s in inputList)
        {
            // Clearing temp variable
            tempWordText = new wordText();

            // Setting universally needed values
            tempWordText.word = s;
            tempWordText.charIndex = index;
            tempWordText.length = s.Length;
            tempWordText.blank = (s[0].Equals("#") && s[s.Length - 1].Equals("#")) ? true : false;

            // topLeft, bottomLeft, bottomRight, topRight
            tempCorners = textBox.GetComponent<TMP_TextInfoDebugTool>().WordCoords(tempWordText.word, tempWordText.charIndex, tempWordText.length);

            tempWordText.rect = new RectTransform();
            tempWordText.rect.sizeDelta = new Vector2((tempCorners[3].x - tempCorners[0].x), (tempCorners[0].y - tempCorners[1].y));

            tempWordText.pos = new Vector3(
                (tempCorners[0].x + tempCorners[1].x + tempCorners[2].x + tempCorners[3].x) / 4.0f,
                (tempCorners[0].y + tempCorners[1].y + tempCorners[2].y + tempCorners[3].y) / 4.0f,
                0.0f
            );

            index += s.Length + 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        List<Vector3> tempCorners;
        foreach (wordText w in words)
        {
            // topLeft, bottomLeft, bottomRight, topRight
            tempCorners = textBox.GetComponent<TMP_TextInfoDebugTool>().WordCoords(w.word, w.charIndex, w.length);

            // TODO: Create a loop that updates the screen coordinates of every word

        }
    }
}
