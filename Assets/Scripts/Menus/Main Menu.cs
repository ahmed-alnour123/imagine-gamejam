using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.lodScene(SeneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Debug.log("Quit");
        Application.Quit();
    }
}