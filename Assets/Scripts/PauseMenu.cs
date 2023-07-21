using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void MiniGame1()
    {
        SceneManager.LoadScene("MiniGame1");
        PauseMenu.GameIsPaused = false;
    }
    public void MiniGame2()
    {
        SceneManager.LoadScene("MiniGame2");
        PauseMenu.GameIsPaused = false;
    }
    public void MiniGame3()
    {
        SceneManager.LoadScene("MiniGame3");
        PauseMenu.GameIsPaused = false;
    }
    public void Map()
    {
        SceneManager.LoadScene("OverworldMap");
        PauseMenu.GameIsPaused = false;
    }

    public void LoadMenu()
    {
        Debug.Log(GameIsPaused);
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}