using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour {

    private Image sprite;
    private pathFinding pathFinding;


    // Start is called before the first frame update
    void Start() {
        sprite = GetComponent<Image>();
        pathFinding = GetComponentInParent<pathFinding>();


    }

    // Update is called once per frame
    void Update() {
        sprite.fillAmount = pathFinding.hp / pathFinding.defaultHP;
        transform.position = Camera.main.WorldToScreenPoint(transform.parent.transform.parent.transform.position);
    }
    public void HPUI(bool _active) {
        sprite.enabled = _active;
    }
}
