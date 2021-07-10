using System.Linq;
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
    public GameObject playMenu;
    private Image noiseCounter;
    private Image hpImage;
    public Text dialogBox;
    public bool isTalking = false;
    public float noise = 0;
    public Transform[] guards;
    public float dangerZone = 20f;
    public bool inDanger;
    public float dist;
    private float playerHP;
    private AudioManager audioManager;
    private Image[] enemyHPbars;
    // Start is called before the first frame update
    private void Awake() {
        eventsManager = this;
    }
    void Start() {
        noiseCounter = playMenu.transform.GetChild(1).GetComponent<Image>();
        hpImage = playMenu.transform.GetChild(3).GetComponent<Image>();
        cameraLook = CameraLook.cameraLook;
        audioManager = AudioManager.audioManager;

        player = Player.player;


    }

    void Update() {
        playerHP = player.hp;
        hpImage.fillAmount = playerHP / player.defaultHP;

        guards = FindObjectsOfType<pathFinding>().Select(path => path.transform).ToArray();
        foreach (var guard in guards) {

            dist = Vector3.Distance(player.transform.position, guard.position);


            if (dist < dangerZone) {
                guard.GetComponentInChildren<HPbar>().HPUI(true);

                if (noise > 99) {
                    audioManager.Play(Sounds.enemyNotice);
                    noise = 0;
                    guard.GetComponent<pathFinding>().detected = true;
                }
                inDanger = true;
                break;

            } else {
                guard.GetComponentInChildren<HPbar>().HPUI(false);
                inDanger = false;

            }
        }
        noiseCounter.fillAmount = noise / 100;
        noise -= Time.deltaTime * 5f;
        if (inDanger) {
            if (player.isMoving && !player.iscrouched) noise += Time.deltaTime * 10;
            if (player.isRunning) noise += Time.deltaTime * 20;
        }
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


    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Player.player.transform.position, dangerZone);
    }

}
