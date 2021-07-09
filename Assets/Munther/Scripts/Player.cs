using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour /* , IHittable*/ {

    [Header("Movement")]
    public bool iscrouched = false;
    public bool isRolling = false;

    public bool isMoving = false;
    public bool isRunning = false;
    public float speed = 5f;
    public float runSpeed = 10f;
    public float crouchHight = 1f;
    public float rollingTime = 0.5f;


    //components and objects
    private Rigidbody rb;
    private CapsuleCollider cl;
    private Transform cameraTarget;
    public static Player player;
    private AudioManager audioManager;
    private Animator animator;

    //Local vars
    public float enemyFreeze = 1;
    public bool justAttacked;
    public float hitCooldown = 2;
    public int defaultHP = 5;
    public int hp = 5;
    float defaultHight;
    float defaultcenter;
    private float timer;
    private Vector3 direction;
    private float coolDown;
    private float crouchTimer;
    private bool wascrouching = false;
    public bool isTargeting;
    public float attackCooldown = 0.5f;
    [HideInInspector]
    public float attackTimer;
    public float hitTimer;


    private void Awake() {

        player = this;
    }

    void Start() {
        audioManager = AudioManager.audioManager;
        rb = GetComponentInParent<Rigidbody>();
        cl = GetComponentInChildren<CapsuleCollider>();
        animator = GetComponentInChildren<Animator>();
        cameraTarget = CameraLook.cameraLook.transform.parent;
        defaultHight = cl.height;
        defaultcenter = cl.center.y;
    }

    void FixedUpdate() {
        if (!iscrouched) crouchTimer = Time.time;
        // While the player is Rolling he cant change direction or speed;
        if (isRolling && timer + rollingTime >= Time.time) {
            rb.AddForce(100 * direction);
            iscrouched = wascrouching;
            coolDown = Time.time + rollingTime;
        } else {
            isRolling = false;
            crouch();
            BasicMovement(speed);
            Run(runSpeed);
            if (isMoving)
                transform.rotation = cameraTarget.transform.rotation;

        }

        if (iscrouched && isRunning && !isRolling && coolDown <= Time.time) {
            Roll();
        }



    }

    void Update() {
        if (hitTimer < Time.time) {
            justAttacked = false;
        } else {
            justAttacked = true;
        }
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isCrouching", iscrouched);
        animator.SetBool("isRolling", isRolling);
    }

    void BasicMovement(float speed) {
        if (!isRunning) {
            if (rb.velocity.magnitude >= speed)
                rb.velocity = rb.velocity.normalized * speed + new Vector3(0, -rb.velocity.y, 0);
            rb.AddForce(100 * transform.forward * Input.GetAxisRaw("Vertical"));
            rb.AddForce(100 * transform.right * Input.GetAxisRaw("Horizontal"));
            if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
                isMoving = false;
            else
                isMoving = true;
        }
    }



    void Investigate(float InvestigateDistance, RaycastHit hit) {
        if (Physics.Raycast(transform.position, transform.forward, out hit, InvestigateDistance)) {
            isTargeting = true;
        } else isTargeting = false;
    }

    void crouch() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            iscrouched = !iscrouched;
        }
        if (iscrouched) {
            cl.center = new Vector3(0, defaultcenter - crouchHight / 2, 0);
            cl.height = defaultHight - crouchHight;
        } else {
            cl.center = new Vector3(0, defaultcenter, 0);
            cl.height = defaultHight;
        }
    }

    void Run(float runSpeed) {
        if (isMoving && Input.GetKey(KeyCode.LeftShift)) {
            isRunning = true;
            if (rb.velocity.magnitude >= runSpeed)
                rb.velocity = rb.velocity.normalized * runSpeed + new Vector3(0, -rb.velocity.y, 0);
            rb.AddForce(100 * transform.forward * Input.GetAxisRaw("Vertical"));
            rb.AddForce(100 * transform.right * Input.GetAxisRaw("Horizontal"));
        } else isRunning = false;
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0) {
            isMoving = false;
        } else {
            isMoving = true;
        }
    }

    void Roll() {
        animator.SetTrigger("isRolling");
        wascrouching = !(crouchTimer + 0.2 > Time.time);
        isRolling = true;
        isRunning = false;
        isMoving = false;
        timer = Time.time;
        direction = transform.forward;
    }

    public void PlayerAttack(GameObject target) {
        player.transform.rotation = cameraTarget.transform.rotation;
        Debug.Log(target.tag);
        if (target.tag == "Enemy") {

            target.GetComponentInParent<pathFinding>().hp--;
            target.GetComponentInParent<pathFinding>().agent.speed = 0;

            StartCoroutine(FreezeFor(enemyFreeze, target));

        }
        attackTimer = Time.time + attackCooldown;
        audioManager.Play(Sounds.swordSwing);
        //audioManager.Play(Sounds.hit);
    }
    IEnumerator FreezeFor(float _freezeSeconds, GameObject target) {

        new WaitForSeconds(_freezeSeconds);
        target.GetComponentInParent<pathFinding>().agent.speed = 5;
        yield return 0;

    }
    // I commented IHittable out.
    public void GetHit() { }
}
