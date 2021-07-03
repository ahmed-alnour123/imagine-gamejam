using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EventsManager : MonoBehaviour {
    private RaycastHit target;
    private GameObject examineText;
    private Player player;
    public float noise = 0;

    void Start() {
        examineText = GameObject.Find("examineText");
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update() {
        noise -= Time.deltaTime * 5f;
        if (player.isMoving && !player.iscrouched) noise += Time.deltaTime * 10;
        if (player.isRunning) noise += Time.deltaTime * 20;

        if (noise > 100) noise = 100;
        if (noise < 0) noise = 0;
        if (player.isTargeting) {
            if (player.hit.collider.tag == "Examine") {
                examineText.SetActive(true);
            } else
                examineText.SetActive(false);
        }
    }
}
