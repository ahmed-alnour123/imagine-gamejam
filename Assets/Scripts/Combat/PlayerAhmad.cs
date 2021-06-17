using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAhmad : MonoBehaviour {
    public static PlayerAhmad player;

    [Header("Movemenet")]
    public float speed;
    public float rotSpeed;

    private float h, v;
    private Rigidbody rb;

    private void Awake() {
        

        player = this;
    }

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        Move();
    }

    private void Move() {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        rb.AddForce(transform.forward * v * speed / 10, ForceMode.Impulse);
        rb.rotation *= Quaternion.Euler(transform.up * h * rotSpeed);
    }
}