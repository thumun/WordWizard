using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{

    public Transform DialogueMenu;
    public GameObject CharDialogue;
    public Transform BtnHolder;
    public Button DialogueClick;
    public Button Optional;
    public Button BtnOne;
    public Button BtnTwo;
    public Button BtnThree;

    private GameObject BlackScreen;
    private bool canClick;

    private NPCCollection dialogue;
    private NPC current;
    private Vector3 newPos;
    private Quaternion newRot;
    private Quaternion CamRot;
    private Quaternion charRot;
    private Vector3 correctPos;
    private Vector3 CamPos;
    private float orthoSize;
    private float targetSize;
    private string dialogueStatus; 

    // Start is called before the first frame update
    void Start()
    {
        BtnHolder.gameObject.SetActive(false);
        DialogueMenu.gameObject.SetActive(false);

        BtnOne.onClick.AddListener(delegate { UserResponse(BtnOne); });
        BtnTwo.onClick.AddListener(delegate { UserResponse(BtnTwo); });
        BtnThree.onClick.AddListener(delegate { UserResponse(BtnThree); });
        Optional.onClick.AddListener(delegate { UserResponse(Optional);});
        DialogueClick.onClick.AddListener(delegate { UserResponse(DialogueClick); });

        SetDialogue.SetLoadFile("overworldDialogue");
        dialogue = new NPCCollection();
        dialogue = SetDialogue.GetDialogueData();

        BlackScreen = GameObject.Find("BlackScreen");
        BlackScreen.SetActive(false);
        canClick = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canClick)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.layer == 6)
                {
                    Debug.Log("clicked on character");
                    StartCoroutine(CharacterDialogue(hit.transform.gameObject));
                }
                else if (hit.transform.gameObject.layer == 7)
                {
                    Debug.Log("clicked on building");
                    StartCoroutine(BuildingZoom(hit.transform.gameObject, BlackScreen));
                    //MiniGameStart(hit.transform.gameObject.name);
                }
            }
        }

        // if need to check building distance
        /*
         * Vector3.Distance( transform.position, otherObject.transform.position) 
         */

    }

    private void InitiateDialogue(string name)
    {
        // based on the name we get the dialogue info
        current = dialogue.getNPC(name);
        current.ResetDialogue();
        AddInfo(current);
    }

    private void UserResponse(Button button)
    {
        if (button.GetComponentInChildren<TextMeshProUGUI>().text.Trim().ToLower() == "goodbye")
        {
            BtnHolder.gameObject.SetActive(false);
            DialogueMenu.gameObject.SetActive(false);
            StartCoroutine(CharacterDialogueEnd());
        } else 
        {
            if (dialogueStatus == "NULL")
            {
                current.DialoguePosition++;
                AddInfo(current);
            }
            else
            {
                DialogueMenu.gameObject.SetActive(false);
            }
            
        } 

    }

    private void AddInfo(NPC current)
    {
        NPCDialogue npc = current.CharDialogue[current.DialoguePosition];
        DialogueClick.gameObject.SetActive(false);

        if (npc.McPhrase.Contains("END"))
        {
            dialogueStatus = "END";
            current.DialoguePosition++;
            CharDialogue.GetComponent<TextMeshProUGUI>().text = npc.CharPhrase;
            DialogueClick.gameObject.SetActive(true); // fix this for END dialogue  
            BtnHolder.gameObject.SetActive(false);
 
            StartCoroutine(CharacterDialogueEnd());
        }
        else if (npc.McPhrase.Contains("NULL"))
        {
            dialogueStatus = "NULL";
            CharDialogue.GetComponent<TextMeshProUGUI>().text = npc.CharPhrase;
            // how to detect click on panel -- > using btn  
            //BtnHolder.gameObject.SetActive(false);

            DialogueClick.gameObject.SetActive(true);
            DialogueMenu.gameObject.SetActive(true);
            BtnHolder.gameObject.SetActive(false);
        }
        else
        {
            CharDialogue.GetComponent<TextMeshProUGUI>().text = npc.CharPhrase;
            if (npc.McPhrase.Count == 2)
            {
                BtnOne.GetComponentInChildren<TextMeshProUGUI>().text = npc.McPhrase[0];
                BtnTwo.GetComponentInChildren<TextMeshProUGUI>().text = npc.McPhrase[1];
                BtnThree.GetComponentInChildren<TextMeshProUGUI>().text = "Goodbye";
                Optional.gameObject.SetActive(false);
            } else
            {
                Optional.GetComponentInChildren<TextMeshProUGUI>().text = npc.McPhrase[0];
                BtnOne.GetComponentInChildren<TextMeshProUGUI>().text = npc.McPhrase[1];
                BtnTwo.GetComponentInChildren<TextMeshProUGUI>().text = npc.McPhrase[2];
                BtnThree.GetComponentInChildren<TextMeshProUGUI>().text = "Goodbye";

                Optional.gameObject.SetActive(true);
            }

            BtnHolder.gameObject.SetActive(true);
            DialogueMenu.gameObject.SetActive(true);
        }

    }

    IEnumerator BuildingZoom(GameObject Building, GameObject blackScreen)
    {
        canClick = false;
        Vector3 CamPos = Camera.main.transform.position;
        Vector3 BuildPos = Building.transform.position;
        Vector3 correctBPos = new Vector3(BuildPos.x - 11.0f, BuildPos.y, BuildPos.z - 11.0f);
        Vector3 newPos = new Vector3(0.0f, 0.0f, 0.0f);

        blackScreen.SetActive(true);
        Color c = blackScreen.GetComponent<Image>().color;
        c.a = 0.0f;
        blackScreen.GetComponent<Image>().color = c;


        for (float f = 0.0f; f < 1.0; f += 0.01f)
        {
            newPos = correctBPos * f + CamPos * (1.0f - f);
            Camera.main.transform.position = new Vector3(newPos.x, CamPos.y, newPos.z);

            c.a = f;
            blackScreen.GetComponent<Image>().color = c;

            yield return new WaitForSeconds(0.01f);
        }
        MiniGameStart(Building.gameObject.name);
        canClick = true;
    }

    IEnumerator CharacterDialogue(GameObject character)
    {
        // FOR THE TIME BEING, CHARACTERS MUST FACE TO THE LEFT
        canClick = false;
        CamPos = Camera.main.transform.position;
        CamRot = Camera.main.transform.rotation;
        charRot = new Quaternion(
            Camera.main.transform.rotation.x - Mathf.PI / 16.0f,
            Camera.main.transform.rotation.y,
            Camera.main.transform.rotation.z + Mathf.PI / 36.0f,
            Camera.main.transform.rotation.w
        );
        orthoSize = Camera.main.orthographicSize;
        targetSize = 3.0f;
        Vector3 charPos = character.transform.position;
        correctPos = new Vector3(charPos.x - 14.0f, charPos.y + 2.6f, charPos.z - 15.0f);

        newPos = new Vector3(0.0f, 0.0f, 0.0f);
        newRot = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

        for (float f = 0.0f; f < 1.0; f += 0.01f)
        {
            newPos = correctPos * f + CamPos * (1.0f - f);
            Camera.main.transform.position = new Vector3(newPos.x, newPos.y, newPos.z);

            newRot = new Quaternion((charRot.x * f + CamRot.x * (1.0f - f)),
                (charRot.y * f + CamRot.y * (1.0f - f)),
                (charRot.z * f + CamRot.z * (1.0f - f)),
                (charRot.w * f + CamRot.w * (1.0f - f))
                );
            Camera.main.transform.rotation = newRot;

            Camera.main.orthographicSize = orthoSize * (1.0f - (f)) + targetSize * (f);
            Debug.Log(Camera.main.orthographicSize);
            yield return new WaitForSeconds(0.01f);
        }

        InitiateDialogue(character.name);
    }

    IEnumerator CharacterDialogueEnd()
    {
        for (float f = 0.0f; f < 1.0; f += 0.01f)
        {
            newPos = correctPos * (1.0f - f) + CamPos * (f);
            Camera.main.transform.position = new Vector3(newPos.x, newPos.y, newPos.z);

            newRot = new Quaternion((CamRot.x * f + charRot.x * (1.0f - f)),
                (CamRot.y * f + charRot.y * (1.0f - f)),
                (CamRot.z * f + charRot.z * (1.0f - f)),
                (CamRot.w * f + charRot.w * (1.0f - f))
                );
            Camera.main.transform.rotation = newRot;

            Camera.main.orthographicSize = targetSize * (1.0f - (f)) + orthoSize * (f);
            Debug.Log(Camera.main.orthographicSize);

            yield return new WaitForSeconds(0.01f);
        }
        Camera.main.transform.position = CamPos;
        Camera.main.transform.rotation = CamRot;
        Camera.main.orthographicSize = orthoSize;

        canClick = true;
    }

    // https://www.youtube.com/watch?v=oNz4I0RfsEg 
    IEnumerator FadeTransition(Transform bg)
    {
        Transform child = bg.GetChild(0);
        Color c = child.GetComponent<Image>().color;
        c.a = 0f;
        child.GetComponent<Image>().color = c;

        bg.gameObject.SetActive(true);

        for (float f = 0.05f; f < 1.05; f += 0.05f)
        {
            c = bg.GetChild(0).GetComponent<Image>().color;
            c.a = f;
            bg.GetChild(0).GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator DialogueFade(NPCDialogue npc)
    {
        CharDialogue.GetComponent<TextMeshProUGUI>().text = npc.CharPhrase;
        yield return new WaitForSeconds(2.0f);
        DialogueMenu.gameObject.SetActive(false);
    }

    private void MiniGameStart(string name)
    {
        if (name == "Academy")
        {
            SceneManager.LoadScene("MiniGame1");
            PauseMenu.GameIsPaused = false;
        }
        else if (name == "cafe")
        {
            SceneManager.LoadScene("MiniGame2");
            PauseMenu.GameIsPaused = false;
        }
        else if (name == "library")
        {
            SceneManager.LoadScene("MiniGame3");
            PauseMenu.GameIsPaused = false;
        }
      
    }
}
