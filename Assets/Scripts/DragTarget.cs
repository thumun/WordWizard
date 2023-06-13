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

    // Submits answer to be checked
    public void checkAnswer()
    {
        foreach (wordBlock block in guessBlocks)
        {
            guess += block.word + " ";
        }
        guess = guess.Remove(guess.Length - 1);
        //correct = guess.Equals(answer);
        Debug.Log("Guess:" + guess + " Answer:" + answer);
    }

    // Clears answer block
    public void clearAnswer()
    {
        anchors = new List<GameObject>();
        guess = "";
        correct = false;
        answerAnchor = new GameObject();
        // In Progress: delete anchors
        foreach (Transform child in target.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        // Making blocks moveable and resetting anchors
        foreach (wordBlock word in guessBlocks)
        {
            word.block.GetComponent<Draggable>().canMove = true;
            word.block.transform.position = word.block.GetComponent<Draggable>().anchorOrigin.transform.position;

        }
        guessBlocks = new List<wordBlock>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
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
