using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * This is a script that works with a winscreen object originally modified from minigame1.
 * The goal is to use a fade in effect accessible from any scene.
 */

public class WinScreen : MonoBehaviour
{
    public GameObject ResetObject;

    public GameObject screenObject;
    private Transform screen;

    // Start is called before the first frame update
    void Start()
    {
        screen = screenObject.GetComponent<Transform>();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeTransition());
    }

    // https://www.youtube.com/watch?v=oNz4I0RfsEg 
    IEnumerator FadeTransition()
    {
        screenObject.gameObject.SetActive(true);
        Transform bg = screen.GetChild(0);

        Color c = bg.GetComponent<Image>().color;
        c.a = 0f;
        bg.GetComponent<Image>().color = c;


        for (float f = 0.00f; f <= 1.01f; f += 0.05f)
        {
            c = bg.GetComponent<Image>().color;
            c.a = f;
            bg.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void quitBtn()
    {
        // perhaps should quit back to the inside of building later

        SceneManager.LoadScene("MainMenu");
        //MAKE SURE TO CALL THE RESPECTIVE RESETGAME FUNCTION FOR THE GIVEN MINIGAME
    }

}
