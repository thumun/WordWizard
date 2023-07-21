using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
