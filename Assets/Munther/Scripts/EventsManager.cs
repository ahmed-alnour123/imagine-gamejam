using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EventsManager : MonoBehaviour {
    public static EventsManager eventsManager;
    private GameObject target;
    public GameObject examineText;
    private CameraLook cameraLook;
    private Player player;
    public Text dialogBox;
    public bool isTalking = false;
    public float noise = 0;
    // Start is called before the first frame update
    private void Awake() {
        eventsManager = this;
    }
    void Start() {
        cameraLook = CameraLook.cameraLook;

        player = Player.player;
    }

    void Update() {
        noise -= Time.deltaTime * 5f;
        if (player.isMoving && !player.iscrouched) noise += Time.deltaTime * 10;
        if (player.isRunning) noise += Time.deltaTime * 20;

        if (noise > 100) noise = 100;
        if (noise < 0) noise = 0;

        if (cameraLook.isTargeting) {
            target = cameraLook.target;

            examineText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) {
                isTalking = true;
            }
        } else {
            examineText.SetActive(false);
        }
    }


}
