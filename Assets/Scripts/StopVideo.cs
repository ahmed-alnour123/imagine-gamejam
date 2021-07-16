using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class StopVideo : MonoBehaviour {
    public VideoPlayer player;
    void Start() { }

    void Update() {
        if (player.frame >= (long) player.frameCount - 1) {
            print("Stopped");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}