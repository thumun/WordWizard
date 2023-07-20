using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    public GameObject BlackScreen;

    // Start is called before the first frame update
    void Start()
    {
        BlackScreen = GameObject.Find("BlackScreen");
        BlackScreen.SetActive(false);
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
    }

    IEnumerator BuildingZoom(GameObject Building, GameObject blackScreen)
    {
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
