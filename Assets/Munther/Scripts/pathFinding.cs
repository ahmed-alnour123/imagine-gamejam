using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pathFinding : MonoBehaviour {
    private NavMeshAgent agent;
    private Player player;
    public float InvestigateDistance = 5f;
    public RaycastHit hit;
    public bool detected = false;
    private float chasingTime = 5;
    private float chaseTimer;
    public float chaseCooldown;
    public float isToutching;
    public Transform defaultpatrollingStart;

    private Vector3 patrollingStart;
    public Transform defaultpatrollingEnd;
    private Vector3 patrollingEnd;

    private bool patrol;
    public static Vector3 raydirection;

    void Start() {
        player = Player.player;
        agent = GetComponent<NavMeshAgent>();
        patrollingEnd = defaultpatrollingEnd.position;
        patrollingStart = defaultpatrollingStart.position;
    }

    void Update() {
        chaseCooldown = chaseTimer - Time.time;

        if (detected) ChasePlayer();
        else Retreat();
        if (Physics.Raycast(transform.position, raydirection, out hit, InvestigateDistance)) {
            if (hit.collider.tag == "PlayerBody") {
                detected = true;
                chaseTimer = Time.time + chasingTime;
            }
        }
    }

    void ChasePlayer() {
        if (chaseTimer < Time.time) {
            detected = false;
            if (isToutching < Time.time) {
                raydirection = transform.forward;
            }
        }
        agent.SetDestination(player.transform.position);
    }

    public void SetDirection(bool b) {

        if (b) raydirection = (player.transform.position - transform.position);
        else raydirection = transform.forward;
    }

    void Retreat() {

        if (transform.position.x == patrollingEnd.x && transform.position.z == patrollingEnd.z) patrol = true;
        if (transform.position.x == patrollingStart.x && transform.position.z == patrollingStart.z) patrol = false;
        if (patrol) agent.SetDestination(patrollingStart);
        else agent.SetDestination(patrollingEnd);
    }
}
