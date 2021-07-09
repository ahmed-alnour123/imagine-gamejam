using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour {
    public float attackRange = 1;
    public float attackCooldown = 2;
    private float attackTimer;
    private float nextAttackTimer;
    private Player player;
    public bool isAttacking;
    private AudioManager audioManager;
    public float distance;
    /// <summary>
    /// the pathFinding component, I made a variable for performance
    /// </summary>
    private pathFinding path;
    private Animator animator;

    void Start() {
        player = Player.player;
        audioManager = AudioManager.audioManager;
        path = GetComponent<pathFinding>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() {
        animator.SetBool("isChasing", path.detected); // update the animation
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (path.detected && distance < attackRange) {
            if (!isAttacking) {
                if (nextAttackTimer + attackCooldown < Time.time) {
                    isAttacking = true;
                    attackTimer = Time.time;
                    nextAttackTimer = Time.time;
                }

            } else {
                if (attackTimer + 0.5f < Time.time) {
                    Attack();
                }

            }
        } else {
            isAttacking = false;
        }


    }

    void Attack() {
        if (!player.justAttacked) {
            player.hp--;
            player.hitTimer = Time.time + player.hitCooldown;
            audioManager.Play(Sounds.getHurt);
        }
        audioManager.Play(Sounds.swordSwing);
        isAttacking = false;
    }
}
