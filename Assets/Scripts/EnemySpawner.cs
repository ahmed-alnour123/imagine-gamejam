using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public float radius;
    public EnemyType enemyType;
    public int count;

    [Header("Assets")]
    public GameObject pig;
    public GameObject guard;

    // private variables
    private GameObject enemy;

    void Awake() {
        switch (enemyType) {
            case EnemyType.pig:
                enemy = pig;
                break;
            case EnemyType.guard:
                enemy = guard;
                break;
        }

        for (int i = 0; i < count; i++) {
            // these two variables to make enemies respawn on the border of a circle not in the middle
            var randomOffset = Random.insideUnitCircle.normalized * radius;
            var offsetVector = new Vector3(randomOffset.x, 0, randomOffset.y);
            Instantiate(
                enemy,
                transform.position + offsetVector,
                Quaternion.Euler(0, Random.Range(0, 360), 0),
                transform
            );
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        // this is just to show the object
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public enum EnemyType { pig, guard }
}
