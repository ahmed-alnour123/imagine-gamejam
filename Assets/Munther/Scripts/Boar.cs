using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Boar : MonoBehaviour {
    private Rigidbody rb;
    private NavMeshAgent agent;
    private pathFinding pathfinding;
    private GameObject player;

    private float Timer = 0;
    private float chargingJump;
    public float jumpCooldown = 3f;
    public bool Targeted = false;
    public bool isJumping = false;
    private bool justJumped = false;
    public float jumpDistance = 15;
    public float jumpPower = 50f;
    Vector3 jumpTarget;

    void Start() {
        rb = GetComponent<Rigidbody>();
        pathfinding = GetComponent<pathFinding>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
    }

    void Update() {
        if (isJumping) Jump();
        else if (Timer + jumpCooldown / 2 < Time.time && justJumped) {
            justJumped = false;
            agent.speed = 3.5f;
        } else {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (pathfinding.detected && Timer + jumpCooldown < Time.time && distance < jumpDistance && !Targeted) {
                Targeted = true;
                chargingJump = Time.time;
                agent.speed = 0;
            } else if (Targeted && chargingJump + 2f < Time.time) {
                jumpTarget = (player.transform.position - transform.position).normalized;
                Jump();
                Timer = Time.time;
                Targeted = false;
            }
        }
    }

    void Jump() {
        isJumping = true;
        if (chargingJump + 3f < Time.time) {
            rb.AddForce(jumpTarget * jumpPower);
            isJumping = false;
            Timer = Time.time + jumpCooldown;
            justJumped = true;
        }
    }
}
