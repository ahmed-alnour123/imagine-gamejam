using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PurpleBoar : MonoBehaviour, IHittable {
    private NavMeshAgent agent;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        if (Vector3.Distance(transform.position, agent.destination) <= 5f) { // if reached the distination
            agent.destination = GetNewDestination();
        }
    }

    private Vector3 GetNewDestination() {
        var navMeshData = NavMesh.CalculateTriangulation();
        int t = Random.Range(0, navMeshData.indices.Length - 3);
        Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], Random.value);
        point = Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], Random.value);
        return point;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "PlayerBody") {
            GameManager.gameManager.foundPig = true;
            Destroy(gameObject);
        }
    }

    public void GetHit() {
    }
}
