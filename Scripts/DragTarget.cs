using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DragTarget : MonoBehaviour
{
    // GameObject of blank space for word blocks
    [SerializeField]
    private GameObject target;
    // Word bank
    [SerializeField]
    private GameObject bank;
    // Drag Button
    [SerializeField]
    private GameObject dragButton;

    // Anchors for the answer blocks to snap to
    [SerializeField]
    private GameObject originAnchor;
    private GameObject answerAnchor;
    private List<GameObject> anchors = new List<GameObject>();

    // string representing actual answer
    [SerializeField]
    private string answer;

    //list of added words to the answer
    private List<wordBlock> guessBlocks = new List<wordBlock>();
    private string guess = "";

    private bool correct = false;

    // Extra variables for fill in the blank mode
    [SerializeField]
    public bool fillInTheBlank = false;
    private List<GameObject> blanks = new List<GameObject>();
    private int blankPos;
    [SerializeField]
    public string inputString;
    private string[] inputStrings;

    // Fill in the blank regular text objects
    GameObject originText;
    GameObject tempText;

    // Adds block to blank
    public void addBlock(wordBlock word)
    {
        answerAnchor = GameObject.Instantiate(originAnchor);
        // Parented to and arranged in horizontal layout on target
        answerAnchor.transform.SetParent(target.transform);

        // Applying horizontal layout
        target.transform.GetComponent<HorizontalLayoutGroup>().CalculateLayoutInputHorizontal();
        target.transform.GetComponent<HorizontalLayoutGroup>().CalculateLayoutInputVertical();
        target.transform.GetComponent<HorizontalLayoutGroup>().SetLayoutHorizontal();
        target.transform.GetComponent<HorizontalLayoutGroup>().SetLayoutVertical();

        // Adding anchor to list of anchors
        anchors.Add(answerAnchor);

        word.curAnchor = answerAnchor;
        word.block.transform.position = answerAnchor.transform.position;
        word.block.GetComponent<Draggable>().canMove = false;
        word.block.GetComponent<Draggable>().anchorCur = answerAnchor;
        guessBlocks.Add(word);
        
    }
    public void fillBlank(wordBlock word)
    {
        answerAnchor = blanks[blankPos];

        word.curAnchor = answerAnchor;
        word.block.transform.position = answerAnchor.transform.position;
        word.block.GetComponent<Draggable>().canMove = false;
        word.block.GetComponent<Draggable>().anchorCur = answerAnchor;
        guessBlocks.Add(word);
        blankPos++;
    }

    // Submits answer to be checked
    public void checkAnswer()
    {
        foreach (wordBlock block in guessBlocks)
        {
            guess += block.word + " ";
        }
        guess = guess.Remove(guess.Length - 1);
        Debug.Log(guess.Equals(answer));
    }

    // Clears answer block
    public void clearAnswer()
    {
        if (!fillInTheBlank)
        {
            anchors = new List<GameObject>();
        }
        guess = "";
        correct = false;
        answerAnchor = new GameObject();
        blankPos = 0;
        if (!fillInTheBlank)
        {
            foreach (Transform child in target.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        // Making blocks moveable and resetting anchors
        foreach (wordBlock word in guessBlocks)
        {
            word.block.GetComponent<Draggable>().canMove = true;
            word.block.GetComponent<Draggable>().anchorCur = word.block.GetComponent<Draggable>().anchorOrigin; 
            word.block.transform.position = word.block.GetComponent<Draggable>().anchorOrigin.transform.position;
        }
        guessBlocks = new List<wordBlock>();

    }

    // Start is called before the first frame update
    void Start()
    {

        if (fillInTheBlank)
        {

            inputStrings = inputString.Split(" ");
            foreach (string s in inputStrings)
            {
                tempText = GameObject.Instantiate(originText);
            }
        }
        else
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (correct)
        {
            clearAnswer();
            Debug.Log("YOU WIN");
        }
        foreach (wordBlock w in bank.GetComponent<DragBank>().buttonBank)
        {
            if (w.block.GetComponent<Draggable>().overTarget)
            {
                addBlock(w);
                w.block.GetComponent<Draggable>().overTarget = false;
            }
        }
        foreach (wordBlock w in guessBlocks)
        {
            w.block.transform.position = w.block.GetComponent<Draggable>().anchorCur.transform.position;
        }
    }
}
