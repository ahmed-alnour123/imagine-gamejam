using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraLook : MonoBehaviour {
    #region PUBLIC FIELDS

    [Header("Mouse Lock")] public static bool isMouseLocked;
    [Header("Camera Field Of View")] public float cameraFieldOfViewMin;
    public float cameraFieldOfViewMax;
    public float fieldOfViewIncrement;
    public float cameraRotateXMin;
    public float cameraRotateXMax;
    public static CameraLook cameraLook;
    public bool hasFruit = false;

    [Header("Mouse Smoothing")] public float mouseSmooth;



    #endregion


    #region PRIVATE FIELDS

    private float m_mouseX;
    private float m_mouseY;
    private float m_rotateX;
    private float m_mouseScrollWheel;
    private Transform m_parent;
    private Transform m_camera;
    private float m_fieldOfView;

    public RaycastHit hit;
    #endregion

    #region UNITY_ROUTINES
    [Header("Investigation")]
    public float InvestigateDistance = 15f;

    public GameObject target;
    public bool isTargeting = false;

    private void Awake() {
        cameraLook = this;
        m_parent = transform.parent;
        m_camera = transform;

        MouseLock();
    }

    private void Update() {
        if (Physics.Raycast(m_camera.transform.position, m_camera.transform.forward, out hit, InvestigateDistance)) {

            if (hit.collider.tag == "Fruit") {
                isTargeting = true;
                if (Input.GetKeyDown(KeyCode.E)) {
                    hit.collider.gameObject.SetActive(false);
                    GameManager.gameManager.foundFlower = true;
                }
            } else if (hit.collider.tag == "ElderHouse") {
                isTargeting = true;
                if (Input.GetKeyDown(KeyCode.E)) {
                    // hit.collider.gameObject.SetActive(false);
                    // GameManager.gameManager.foundElderHouse = true;
                }
            } else if (hit.collider.tag == "Examine") {
                target = hit.collider.gameObject;
                isTargeting = true;
            } else {
                isTargeting = false;
            }
        } else {
            isTargeting = false;

        }
        if (!PauseMenu.gameIsPaused) {
            MouseInput();
            RotatePlayY();
            RotateCameraX();
            isMouseLocked = true;
        } else {
            isMouseLocked = false;
        }
        MouseLock();
    }

    #endregion


    private void MouseInput() {
        m_mouseX = Input.GetAxisRaw("Mouse X") * mouseSmooth;
        m_mouseY = Input.GetAxisRaw("Mouse Y") * mouseSmooth;
        m_mouseScrollWheel = Input.GetAxisRaw("Mouse ScrollWheel");
    }

    private void RotatePlayY() {
        m_parent.Rotate(Vector3.up * m_mouseX);
    }

    private void RotateCameraX() {
        m_rotateX += m_mouseY;
        m_rotateX = Mathf.Clamp(m_rotateX, cameraRotateXMin, cameraRotateXMax);
        m_camera.transform.localRotation = Quaternion.Euler(-m_rotateX, 0f, 0f);
    }

    private void MouseLock() {
        if (isMouseLocked) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            return;
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
