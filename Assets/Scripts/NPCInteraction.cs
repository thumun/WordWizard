using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.layer == 6)
                {
                    Debug.Log("clicked on character");
                }
                else if (hit.transform.gameObject.layer == 7)
                {
                    Debug.Log("clicked on building");
                    MiniGameStart(hit.transform.gameObject.name);
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
