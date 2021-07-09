using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraLook : MonoBehaviour {
    #region PUBLIC FIELDS

    [Header("Mouse Lock")] public bool isMouseLocked;
    [Header("Camera Field Of View")] public float cameraFieldOfViewMin;
    public float cameraFieldOfViewMax;
    public float fieldOfViewIncrement;
    public float cameraRotateXMin;
    public float cameraRotateXMax;
    public static CameraLook cameraLook;

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
    private GameObject pausemenu;

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

            if (hit.collider.tag == "Examine") {
                if (target != null && target != hit.collider.gameObject) {
                    target.GetComponent<Dialogs>().enabled = false;
                    target.GetComponent<Dialogs>().index = 0;
                }

                target = hit.collider.gameObject;
                target.GetComponent<Dialogs>().enabled = true;
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
        }
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
            Cursor.lockState = CursorLockMode.Confined;
#if UNITY_EDITOR // to fix our linux and unity problem
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
#endif
            return;
        }
        Cursor.lockState = CursorLockMode.None;
    }
}
