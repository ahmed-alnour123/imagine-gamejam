using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pathFinding : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
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
    private   Vector3 raydirection;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrollingEnd = defaultpatrollingEnd.position;
        patrollingStart = defaultpatrollingStart.position;
    }

    // Update is called once per frame
    void Update()
    {
        chaseCooldown = chaseTimer - Time.time;

        try{        player = coneMesh.player;

            raydirection =  (player.transform.position - transform.position).normalized;
        } catch(System.NullReferenceException ex){
            Debug.Log(ex.Message);

        }
        if(detected) ChasePlayer();
        else Retreat();
        if (Physics.Raycast(transform.position,raydirection , out hit,InvestigateDistance)){
       
            Debug.Log(hit.collider.name);
        }
        if(hit.collider.tag == "PlayerBody"){
            
            Debug.Log("second stage");


            detected = true;
            chaseTimer = Time.time + chasingTime;
               }
            
        
        }
    
    

    void ChasePlayer(){

        if(chaseTimer  < Time.time) {
            
            detected = false;
            
            if(isToutching < Time.time)  coneMesh.player = null;

            }

        agent.SetDestination(player.transform.position);

        
    }


    void Retreat() {         
           Debug.Log("ssss stage");

        if (transform.position.x == patrollingEnd.x && transform.position.z == patrollingEnd.z) patrol = true;
        if (transform.position.x == patrollingStart.x && transform.position.z == patrollingStart.z) patrol = false;
        
        if(patrol)  agent.SetDestination(patrollingStart);
            else    agent.SetDestination(patrollingEnd);
    }
}
