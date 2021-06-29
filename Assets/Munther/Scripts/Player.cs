using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
 

    [Header("Movement")]
    public bool iscrouched = false;
    public bool isRolling = false;

    public bool isMoving = false;
    public bool isRunning = false;
    public float speed = 5f;
    public float runSpeed = 10f;
    public float crouchHight = 1f;
    public float rollingTime = 0.5f;

    [Header("Investigation")]
    public float InvestigateDistance = 5f;
    public bool isTargeting = false;
    public RaycastHit hit;

//components and objects

    private Rigidbody rb;
    private CapsuleCollider cl;
    private Transform cameraTarget;

//Local vars
    private float _lookx = 0;
    private float _looky = 0;
    float defaultHight;
    float defaultcenter;
    private float timer;
    private Vector3 direction;
    private float coolDown;
    private float crouchTimer;
    private bool wascrouching = false;
    public static Player player;
    // Start is called before the first frame update
    private void Awake() {
        player = this;

    }
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        cl = GetComponentInChildren<CapsuleCollider>();
        cameraTarget = transform.GetChild(0).transform;
        defaultHight = cl.height;
        defaultcenter = cl.center.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    
    {
        
        Cursor.visible = false;
        if(!iscrouched) crouchTimer = Time.time;
// While the player is Rolling he cant change direction or speed;
        if(isRolling && timer + rollingTime >= Time.time){
            rb.AddForce(100 * direction);
            iscrouched = wascrouching;
            coolDown = Time.time + rollingTime;
        } 
        else{
        isRolling = false;
        crouch();
        Investigate(InvestigateDistance,hit);
       // Rotation();
        basic_movement(speed);
        Run(runSpeed);
        if(isMoving)
        transform.rotation = cameraTarget.transform.rotation;
        
    }

if (iscrouched && isRunning && !isRolling && coolDown  <= Time.time)
        {
            Roll();
            
        }
    }

    void basic_movement(float speed){
        if(!isRunning){
        if(rb.velocity.magnitude >= speed)
        rb.velocity = rb.velocity.normalized * speed;
        rb.AddForce(100 * transform.forward * Input.GetAxisRaw("Vertical"));
        rb.AddForce(100 * transform.right * Input.GetAxisRaw("Horizontal"));
        if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        isMoving = false;
        else 
        isMoving = true;
    }}

    void Rotation(){
        _lookx = Input.GetAxis("Mouse X");
        _looky = Input.GetAxis("Mouse Y");

        cameraTarget.transform.rotation *= Quaternion.AngleAxis(5f*_lookx,Vector3.up);
        

    }

    void Investigate(float InvestigateDistance , RaycastHit hit){
         if (Physics.Raycast(transform.position, transform.forward, out hit, InvestigateDistance)) 
         {
             isTargeting = true;
             
             Debug.Log(hit.collider.tag);
         } else isTargeting = false;
    }
    
    void crouch(){
        if(Input.GetKeyDown(KeyCode.Space)){
            iscrouched = !iscrouched;     
        }
        
        if(iscrouched) {
            cl.center = new Vector3(0,defaultcenter-crouchHight/2,0);
            cl.height = defaultHight - crouchHight;
        } else{
            cl.center = new Vector3(0,defaultcenter,0);
            cl.height = defaultHight;
        }
    }
    void Run(float runSpeed){
        if(isMoving && Input.GetKey(KeyCode.LeftShift)){
                    isRunning = true;

        if(rb.velocity.magnitude >= runSpeed)
        rb.velocity = rb.velocity.normalized * runSpeed;
        rb.AddForce(100 * transform.forward * Input.GetAxisRaw("Vertical"));
        rb.AddForce(100 * transform.right * Input.GetAxisRaw("Horizontal"));
        } else isRunning = false;
        
        if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0){
        isMoving = false;}
        else {
        isMoving = true;}
    }
    void Roll(){
        wascrouching = !(crouchTimer +0.2 > Time.time); 
        isRolling = true;
        isRunning = false;
        isMoving = false;
        timer = Time.time;
        direction = transform.forward;
    }

}
