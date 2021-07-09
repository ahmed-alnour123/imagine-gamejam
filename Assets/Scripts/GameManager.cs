using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager gameManager;

    Player player;

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

        // if (foundFlower && foundPurplePig && reachedHome){
        //     Win();
        // }
    }

    void Win() {
        Time.timeScale = 0;
        PauseMenu.gameIsPaused = true;
        // Todo: show win menu
    }

    void Lose() {
        Time.timeScale = 0;
        PauseMenu.gameIsPaused = true;
        // Todo: show lose menu
    }
}
