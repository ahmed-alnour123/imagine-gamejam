using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Todo: check if player or enemy
public class Combat : MonoBehaviour {
    public int maxHealth;
    // [HideInInspector]
    public int currentHealth;
    public int hitDamage;
    public float radius;
    public KeyCode attackButton = KeyCode.Space;
    public EnemyType enemyType; // type to distiguishe player from enemies

    public enum EnemyType { player, enemy }

    // public event Action<float> onAttack = delegate { };

    private void Start() {

    }

    private void Update() {
        if (CheckAttack()) {
            Attack();
        }
    }

    /// checks if attack conditions are met
    bool CheckAttack() {
        switch (enemyType) {
            case EnemyType.player:
            case EnemyType.enemy:
                return Input.GetKeyDown(attackButton);
        }
        return false;
    }

    /// checks if taking damage conditions are met
    bool CheckTakeDamage() {
        switch (enemyType) {
            case EnemyType.player:
                break;
            case EnemyType.enemy:
                break;
        }
        return true;
    }

    /// attack the target based on type
    public void Attack() {
        switch (enemyType) {
            case EnemyType.player:
                break;
            case EnemyType.enemy:
                break;
        }
        foreach (var collider in Physics.OverlapSphere(transform.position, radius)) {
            var other = collider.gameObject.GetComponent<Combat>();
            // make sure that other is not null or this object or of the same type
            if (other == null || other == this || other.enemyType == enemyType) continue;
            other.TakeDamage();
        }
    }

    /// take damage based on attaking object type
    public void TakeDamage() {
        if (!CheckTakeDamage()) return;
        currentHealth -= hitDamage;
        switch (enemyType) {
            case EnemyType.player:
                GetComponent<MeshRenderer>().material.color = Color.white * (currentHealth / maxHealth);
                break;
            case EnemyType.enemy:
                GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
                break;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}