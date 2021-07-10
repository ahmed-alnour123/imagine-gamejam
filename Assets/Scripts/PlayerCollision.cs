using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Flower") {
            GameManager.gameManager.foundFlower = true;
            Destroy(other.gameObject);
        } else if (other.tag == "ElderHouse") {
            print("I found Elder");
            GameManager.gameManager.foundElderHouse = true;
        }
    }
}
