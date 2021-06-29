using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EventsManager : MonoBehaviour
{
    private RaycastHit target;
    public GameObject examineText;
    private CameraLook cameraLook;
    private Player player;
    public float noise = 0;
    // Start is called before the first frame update
    void Start()
    {
        cameraLook = CameraLook.cameraLook;

    player = Player.player;
    }

    // Update is called once per frame
    void Update()
    {
        noise -= Time.deltaTime * 5f;
        if(player.isMoving && !player.iscrouched) noise += Time.deltaTime * 10;
        if(player.isRunning) noise += Time.deltaTime * 20;

        if(noise > 100) noise = 100;
        if(noise < 0) noise = 0;

        if (cameraLook.isTargeting)
        {

            examineText.SetActive(true);
        }else{
        examineText.SetActive(false);
        }
        
    }
}
