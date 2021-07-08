using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    private Player player;
    private void Start() {

        player = Player.player;

    }
    private void Update() {
    }
    
    private void OnTriggerStay(Collider other) {
        if( player.attackTimer < Time.time && Input.GetKeyDown(KeyCode.Mouse0)){
            player.PlayerAttack(other.gameObject);
            
        }
    }
}
