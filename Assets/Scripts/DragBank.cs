using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * Goal:
 * This object is a ui element where draggable words snap to.
 * The structures should keep track of the info of words (class instead?)
 * This ui element only creates and moves the anchors
 * Maybe put the draggable ui into a class, and add structure info, then
 * add snapping.
 */

public struct wordBlock
{
    public string word;
    public GameObject block;

    public float width;
    public float height;
    public Vector2 originAnchor;
    public Vector2 curAnchor;
}

public class DragBank : MonoBehaviour
{
    // Original block and anchor for duplication
    [SerializeField]
    private GameObject originBlock;
    [SerializeField]
    private GameObject originAnchor;

    // The game object this script controls
    [SerializeField]
    public GameObject blockBank;

    // The words used in the puzzle
    [SerializeField]
    private List<string> wordBank;

    // Array of buttons
    public List<wordBlock> buttonBank = new List<wordBlock>();

    // Variables for the loop
    public wordBlock tempBlock;

    private RectTransform tempRT;

    private Vector3 canvasPosition = new Vector3(0.0f, 0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        foreach (string s in wordBank)
        {
            // Clearing the temp
            tempBlock = new wordBlock();

            // Setting the word to be equal to the string
            tempBlock.word = s;

            // Creating the game object
            tempBlock.block = GameObject.Instantiate(originBlock);

            tempBlock.block.transform.SetParent(blockBank.transform);

            
            // Takes the text box and changes it to the word
            tempBlock.block.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().SetText(s);
            
            // Finds the RectTransform of the button
            tempRT = (RectTransform)tempBlock.block.transform;
            // Uses RectTransform to get width and height
            tempBlock.width = tempRT.rect.width;
            tempBlock.height = tempRT.rect.height;
            
            /*
             * In the future, canvas position will begin at top left, and go right,
             * then go down rows when width has been reached. For now,
             * it will move a set amount so I can test things.
             */
            
            // Setting position of anchors
            tempBlock.originAnchor = canvasPosition;
            tempBlock.curAnchor = canvasPosition;

            buttonBank.Add(tempBlock);

            canvasPosition += new Vector3(10.0f, 0.0f, 0.0f);
            
        }   

    }

    // Update is called once per frame
    void Update()
    {
    }
}
