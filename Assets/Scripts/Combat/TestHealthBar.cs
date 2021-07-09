using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHealthBar : MonoBehaviour {
    private pathFinding guard;
    private Transform foreground;

    private void Start() {
        guard = GetComponentInParent<pathFinding>();
        foreground = transform.GetChild(1);
        print(foreground.name);
    }

    void Update() {
        foreground.localScale = new Vector3(guard.hp / guard.defaultHP, foreground.localScale.y, foreground.localScale.z);
    }
}
