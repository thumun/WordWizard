using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 


public class MiniMenuFS : MonoBehaviour, IPointerDownHandler
{

    NPCCollection dialogue;

    public Button one;
    public Button two;
    public Button three;

    public Transform NPCDialogueBtns;
    public Transform UserBtns; 

    // Start is called before the first frame update
    void Start()
    {
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

    }

    void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name);
        GetCharDialogue(this.gameObject.name);
        
    }

    void GetCharDialogue(string name)
    {
        Button currNPCBtn = NPCDialogueBtns.Find(name + "Btn").GetComponent<Button>();
        NPC current = dialogue.getNPC(name);

        foreach (NPCDialogue npc in current.CharDialogue)
        {
            if (npc.McPhrase.Contains("END"))
            {
                // don't set active MC response
                // leave npc response bubble there for a bit??
                // ienumerator perhaps
                StartCoroutine(RidNPCDialogue(currNPCBtn, npc));
                UserBtns.gameObject.SetActive(false);

            } else if (npc.McPhrase.Contains("NULL"))
            {
                // don't set active MC response
                currNPCBtn.GetComponentInChildren<Text>().text = npc.CharPhrase;
                //currNPCBtn.gameObject.SetActive(true);
                UserBtns.gameObject.SetActive(false);

            } else
            {
                currNPCBtn.GetComponentInChildren<Text>().text = npc.CharPhrase;

                one.gameObject.GetComponentInChildren<Text>().text = npc.McPhrase[0];
                two.gameObject.GetComponentInChildren<Text>().text = npc.McPhrase[1];

                if (npc.McPhrase.Count == 2)
                {
                    three.gameObject.SetActive(false);
                } else
                {
                    three.gameObject.GetComponentInChildren<Text>().text = npc.McPhrase[2];
                    three.gameObject.SetActive(true);
                }

                UserBtns.gameObject.SetActive(true);
                currNPCBtn.gameObject.SetActive(true);

            }
        }

        
    }

    void UserResponse(Button button)
    {
        if (button.GetComponentInChildren<Text>().text == "Goodbye")  
        {
            Debug.Log("End dialogue");
            UserBtns.gameObject.SetActive(false);

        }

    }

    IEnumerator RidNPCDialogue(Button currNPCBtn, NPCDialogue npc)
    {
        currNPCBtn.GetComponentInChildren<Text>().text = npc.CharPhrase;
        yield return new WaitForSeconds(0.05f);
        currNPCBtn.gameObject.SetActive(false);
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
