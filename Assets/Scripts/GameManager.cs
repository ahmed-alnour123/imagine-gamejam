using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager gameManager;

    [Header("Player")]
    Player player;
    public bool foundPig = false;
    public bool foundFlower = false;
    public bool foundElderHouse = false;

    [Header("UI")]
    public GameObject winMenu;
    public GameObject loseMenu;

    void Awake() {
        gameManager = this;
    }

    void Start() {
        player = Player.player;
    }

    void Update() {
        if (player.hp == 0) {
            Lose();
        }

        if (foundFlower && foundPig && foundElderHouse) {
            Win();
        }
    }

    void Win() {
        Time.timeScale = 0;
        PauseMenu.gameIsPaused = true;
        // Todo: show win menu
        winMenu.SetActive(true);
        print("Won");
    }

    void Lose() {
        Time.timeScale = 0;
        PauseMenu.gameIsPaused = true;
        // Todo: show lose menu
        loseMenu.SetActive(true);
        print("Lost");
    }
}
