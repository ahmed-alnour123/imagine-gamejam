using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public float attackRange = 1;
    public float attackCooldown = 2;
    private float attackTimer;
    private float nextAttackTimer;
    private Player player;
    public bool isAttacking;
    private AudioManager audioManager;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.player;
        audioManager = AudioManager.audioManager;

    }

    // Update is called once per frame
    void Update() {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (GetComponent<pathFinding>().detected && distance < attackRange ) {
            if (!isAttacking) {
                if (nextAttackTimer + attackCooldown < Time.time) {
                    isAttacking = true;
                    attackTimer = Time.time;
                    nextAttackTimer = Time.time;
                }

            } else {
                if(attackTimer + 0.5f < Time.time){
                    Attack();
                }

            }
        } else{
            isAttacking = false;
        }


    }
    void Attack(){
        if (!player.justAttacked) {
            player.hp--;
            player.hitTimer = Time.time + player.hitCooldown;
            audioManager.Play(Sounds.getHurt);
        }
        audioManager.Play(Sounds.swordSwing);
        isAttacking = false;
    }
}
