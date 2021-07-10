using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject plauMenuUI;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        plauMenuUI.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause() {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        plauMenuUI.SetActive(false);

        gameIsPaused = true;
    }

    public void LoadMenu() {
        SceneManager.LoadScene("MainMenu");

    }

    public void QuitGame() {
        Application.Quit();
    }
}
