using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogs : MonoBehaviour {
    private EventsManager eventsmanager;
    private CameraLook cameraLook;
    private Text dialogBox;
    [TextArea(2, 5)]
    public string[] sentences;
    public int index;
    public bool isisTalking = false;
    private bool gameIsPaused;
    private bool done = true;
    public bool isElder = false;
    private string[] notFoundOthers = new string[] {
                    "find me things",
                    "fast",
                    "end"
                  };
    private string[] foundOthers = new string[] {
                    "good",
                    "ok",
                    "..."
                };

    private void Start() {
        cameraLook = CameraLook.cameraLook;
        eventsmanager = EventsManager.eventsManager;
        dialogBox = eventsmanager.dialogBox;
        if (isElder) {
            sentences = (GameManager.gameManager.foundFlower && GameManager.gameManager.foundPig) ? foundOthers : notFoundOthers;
        }
    }

    private void Update() {
        if (isElder) {
            sentences = (GameManager.gameManager.foundFlower && GameManager.gameManager.foundPig) ? foundOthers : notFoundOthers;
        }
        gameIsPaused = PauseMenu.gameIsPaused;

        if (cameraLook.isTargeting && Input.GetKeyDown(KeyCode.E) && !isisTalking) {
            eventsmanager.isTalking = true;
            isisTalking = true;
            dialogBox.gameObject.SetActive(true);
        }


        if (Input.GetKeyDown(KeyCode.E) && isisTalking) {
            NextDialog();
        }

    }
    IEnumerator Type(int _index) {
        done = false;
        dialogBox.text = "";

        if (sentences[_index] == "...") {
            GameManager.gameManager.foundElderHouse = true;
        }

        foreach (char letter in sentences[_index].ToCharArray()) {
            dialogBox.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        done = true;
    }

    void NextDialog() {


        if (index == sentences.Length) {
            eventsmanager.isTalking = false;
            index = 0;
            dialogBox.text = "";
            isisTalking = false;
            dialogBox.gameObject.SetActive(false);


        } else if (done) {
            index++;

            StartCoroutine(Type(index - 1));
        }
    }

}
