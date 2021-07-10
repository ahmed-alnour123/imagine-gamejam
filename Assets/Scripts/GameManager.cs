using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager gameManager;

    Player player;
    public bool foundPig = false;
    public bool foundFlower = false;
    public bool foundElderHouse = false;

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
        print("Won");
    }

    void Lose() {
        Time.timeScale = 0;
        PauseMenu.gameIsPaused = true;
        // Todo: show lose menu
        print("Lost");
    }
}
