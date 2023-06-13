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

// Represents the info associated with a draggable ui word block
public struct wordBlock
{
    public string word;
    public GameObject block;

    public float width;
    public float height;
    public GameObject originAnchor;
    public GameObject curAnchor;
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
    public float bankWidth;
    public float bankHeight;

    // The overall draggable area
    [SerializeField]
    public GameObject dragArea;

    // The words used in the puzzle
    [SerializeField]
    private List<string> wordBank;

    // Array of buttons
    public List<wordBlock> buttonBank = new List<wordBlock>();

    // Variables for the loop
    public wordBlock tempBlock;
    public GameObject tempAnchor;
    private RectTransform tempRT;
    private int count = 0;

    // Setting start for canvas position
    private Vector3 canvasPosition = new Vector3(0.0f, 60.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        // Get width and height of bank
        tempRT = (RectTransform)blockBank.transform;
        bankWidth = tempRT.rect.width;
        bankHeight = tempRT.rect.height;
        // Setting back to null so we don't get undetectable errors
        tempRT = null;
        foreach (string s in wordBank)
        {
            // Clearing the temp
            tempBlock = new wordBlock();
            tempAnchor = null;

            // Setting the word to be equal to the string
            tempBlock.word = s;

            // Creating the "word block"
            tempBlock.block = GameObject.Instantiate(originBlock);
            // Parented to draggable area
            tempBlock.block.transform.SetParent(dragArea.transform);
            // Activate block
            tempBlock.block.SetActive(true);

            // Creating anchor
            tempAnchor = GameObject.Instantiate(originAnchor);
            // Parented to and arranged in grid on bank
            tempAnchor.transform.SetParent(blockBank.transform);

            // Applying grid layout
            blockBank.transform.GetComponent<GridLayoutGroup>().CalculateLayoutInputHorizontal();
            blockBank.transform.GetComponent<GridLayoutGroup>().CalculateLayoutInputVertical();
            blockBank.transform.GetComponent<GridLayoutGroup>().SetLayoutHorizontal();
            blockBank.transform.GetComponent<GridLayoutGroup>().SetLayoutVertical();

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

            tempBlock.originAnchor = tempBlock.curAnchor = tempAnchor;

            // Setting anchor in code
            // tempBlock.block.GetComponent<Draggable>().setAnchorOrigin(tempBlock.originAnchor);
            tempBlock.block.GetComponent<Draggable>().anchorOrigin = tempBlock.originAnchor;
            tempBlock.block.GetComponent<Draggable>().setAnchorCur(tempBlock.curAnchor);

            // Snapping block to anchor
            tempBlock.block.transform.position = tempAnchor.transform.position;

            // Changing scale to 1 for anchor and block (WHYYYYYY?????)
            tempBlock.block.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            tempAnchor.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            buttonBank.Add(tempBlock);

            count++;
        }   

    }

    // Update is called once per frame
    void Update()
    {
        foreach (wordBlock b in buttonBank)
        {

        }
    }
}
