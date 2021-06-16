using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                PauseMenu();
            }
        }
    }
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timescale = 1f;
        GameIsPaused = false;

    }
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timescale = 0f;
        GameIsPaused = true;
    }
    public void LodMenu()
    {
        debug.log("Loding menu...");
    }
    public void QuitGame()
    {
        debug.log("Qutting game...");
    }
}
