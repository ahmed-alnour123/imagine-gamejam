using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogs : MonoBehaviour
{
    private EventsManager eventsmanager;
    private CameraLook cameraLook;
    private Text dialogBox;
    public string[] sentences;
    public int index;
    public bool isisTalking = false;
    private bool gameIsPaused;

    private void Start()
    {
        cameraLook = CameraLook.cameraLook;
        eventsmanager = EventsManager.eventsManager;
        dialogBox = eventsmanager.dialogBox;
    }

    private void Update()
    {
        gameIsPaused = PauseMenu.gameIsPaused;

        if (cameraLook.isTargeting && Input.GetKeyDown(KeyCode.E) && !isisTalking)
        {
            eventsmanager.isTalking = true;
            isisTalking = true;
            dialogBox.gameObject.SetActive(true);
        }


        if (Input.GetKeyDown(KeyCode.E) && isisTalking)
        {
            NextDialog();
        }

    }
    IEnumerator Type(int _index)
    {

        dialogBox.text = "";

        foreach (char letter in sentences[_index].ToCharArray())
        {
            dialogBox.text += letter;
            yield return new WaitForSeconds(0.02f);


        }
    }

    void NextDialog()
    {
        index++;


        if (index == sentences.Length + 1)
        {
            eventsmanager.isTalking = false;
            index = 0;
            dialogBox.text = "";
            isisTalking = false;
            dialogBox.gameObject.SetActive(false);


        }
        else
        {
            StartCoroutine(Type(index - 1));
        }
    }

}
