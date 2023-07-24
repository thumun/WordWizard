using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MG3MiniMenu : MonoBehaviour
{
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    public Button YesBtn;
    public Button NoBtn;
    public Transform DialogueContainer; 

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();

        YesBtn.onClick.AddListener(StartGame);
        NoBtn.onClick.AddListener(CloseDialogue);
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
                
                if (result.gameObject.layer == 6)
                {
                    Debug.Log(result.gameObject.name);
                    DialogueContainer.gameObject.SetActive(true);
                    //break;
                }

            }
        }


    }

    void StartGame()
    {
        // trigger scene switcher
        SceneManager.LoadScene("MiniGame3");
    }

    void CloseDialogue()
    {
        DialogueContainer.gameObject.SetActive(false);
    }

   
}
