using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject cube;
    public int number;
    public int radius;

    void Start() {
        for (int i = 0; i < number; i++) {
            var point = Random.insideUnitCircle * radius;
            Instantiate(cube, new Vector3(point.x, 1 / 2f, point.y), Quaternion.identity, transform);
        }
    }
}