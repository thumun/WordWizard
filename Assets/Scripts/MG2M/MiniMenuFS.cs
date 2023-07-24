using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MiniMenuFS : MonoBehaviour
{

    NPCCollection dialogue;

    public Button one;
    public Button two;
    public Button three;

    public Transform NPCDialogueBtns;
    public Transform UserBtns;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    private Button currNPCBtn;
    private NPC current;

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();

        SetDialogue.SetLoadFile("dialogueMG2");
        dialogue = new NPCCollection();
        dialogue = SetDialogue.GetDialogueData();

        one.onClick.AddListener(delegate { UserResponse(one); });
        two.onClick.AddListener(delegate { UserResponse(two); });
        three.onClick.AddListener(delegate { UserResponse(three); });

        one.gameObject.SetActive(true);
        two.gameObject.SetActive(true);
        three.gameObject.SetActive(true);
        UserBtns.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                //Debug.Log("Hit " + result.gameObject.name);

                /*
                if (result.gameObject.name == "Frog"
                    || result.gameObject.name == "Barista"
                    || result.gameObject.name == "ShibaSmall"
                    || result.gameObject.name == "ShibaBig"
                    || result.gameObject.name == "Dragon"
                    || result.gameObject.name == "Bird")
                {
                    Debug.Log(result.gameObject.name);
                    //GetCharDialogue(this.gameObject.name);
                    //break;
                }
                */

                // this is not working

                if (result.gameObject.layer == 6)
                {
                    Debug.Log(result.gameObject.name);
                    GetCharDialogue(result.gameObject.name);
                    //break;
                }

            }
        }


    }

    /*
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(name);
        //GetCharDialogue(this.gameObject.name);
        
    }
    */

    void GetCharDialogue(string name)
    {
        currNPCBtn = NPCDialogueBtns.Find(name + "Btn").GetComponent<Button>();

        // bit of hard coding for frog so we can edit what he's saying here
        // instead of in the data 
        if (name.Trim().ToLower() == "frog")
        {
            FrogSettings(currNPCBtn);
        } else
        {
            current = dialogue.getNPC(name);
            current.DialoguePosition = 0;

            AddingInfo(current, currNPCBtn);
        }

    }

    void UserResponse(Button button)
    {
        // why isn't this working 
        if (button.GetComponentInChildren<TextMeshProUGUI>().text.Trim().ToLower() == "goodbye"
            || button.GetComponentInChildren<TextMeshProUGUI>().text.Trim().ToLower() == "no")
        {
            Debug.Log("End dialogue");
            UserBtns.gameObject.SetActive(false);
            currNPCBtn.gameObject.SetActive(false);
        }
        else if (button.GetComponentInChildren<TextMeshProUGUI>().text.Trim().ToLower() == "yes")
        {
            SceneManager.LoadScene("MiniGame2");
        }
        else
        {
            current.DialoguePosition++;
            // make responses disappear? 
            AddingInfo(current, currNPCBtn);
        }
    }

    void AddingInfo(NPC current, Button currNPCBtn)
    {
        NPCDialogue npc = current.CharDialogue[current.DialoguePosition];
        if (npc.McPhrase.Contains("END"))
        {
            // don't set active MC response
            // leave npc response bubble there for a bit??
            // ienumerator perhaps
            StartCoroutine(RidNPCDialogue(currNPCBtn, current));
            UserBtns.gameObject.SetActive(false);

        }
        else if (npc.McPhrase.Contains("NULL"))
        {
            UserBtns.gameObject.SetActive(false);
            StartCoroutine(MultipleDialogue(currNPCBtn, current));
            
        }
        else
        {
            currNPCBtn.GetComponentInChildren<TextMeshProUGUI>().text = npc.CharPhrase;

            one.GetComponentInChildren<TextMeshProUGUI>().text = npc.McPhrase[0];
            two.GetComponentInChildren<TextMeshProUGUI>().text = npc.McPhrase[1];

            if (npc.McPhrase.Count == 2)
            {
                three.gameObject.SetActive(false);
            }
            else
            {
                three.GetComponentInChildren<TextMeshProUGUI>().text = npc.McPhrase[2];
                three.gameObject.SetActive(true);
            }

            UserBtns.gameObject.SetActive(true);
            currNPCBtn.gameObject.SetActive(true);

        }
    }


    IEnumerator RidNPCDialogue(Button currNPCBtn, NPC npc)
    {
        currNPCBtn.GetComponentInChildren<TextMeshProUGUI>().text = npc.CharDialogue[npc.DialoguePosition].CharPhrase;
        yield return new WaitForSeconds(2.0f);
        currNPCBtn.gameObject.SetActive(false);
    }

    // not a smart way to handle this --> assumes that there is only one line after NULL line 
    IEnumerator MultipleDialogue(Button currNPCBtn, NPC npc)
    {
        currNPCBtn.GetComponentInChildren<TextMeshProUGUI>().text = npc.CharDialogue[npc.DialoguePosition].CharPhrase;
        current.DialoguePosition++;
        yield return new WaitForSeconds(1.5f);
        yield return RidNPCDialogue(currNPCBtn, npc);

    }

    void FrogSettings(Button currNPCBtn)
    {
        currNPCBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Oh dear.. I really don't know how to fix the mistakes in my writing.. Can you help? (Are you ready to start the mini game?)";
        one.GetComponentInChildren<TextMeshProUGUI>().text = "Yes";
        two.GetComponentInChildren<TextMeshProUGUI>().text = "No";
        three.gameObject.SetActive(false);
        UserBtns.gameObject.SetActive(true);
        currNPCBtn.gameObject.SetActive(true);
    }


}
