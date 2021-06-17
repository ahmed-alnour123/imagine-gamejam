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
    public EntityType entityType; // type to distiguishe player from enemies

    public enum EntityType { player, enemy }

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
        switch (entityType) {
            case EntityType.player:
            case EntityType.enemy:
                return Input.GetKeyDown(attackButton);
        }
        return false;
    }

    /// checks if taking damage conditions are met
    bool CheckTakeDamage() {
        switch (entityType) {
            case EntityType.player:
                break;
            case EntityType.enemy:
                break;
        }
        return true;
    }

    /// attack the target based on type
    public void Attack() {
        switch (entityType) {
            case EntityType.player:
                var audioManager = AudioManager.audioManager;

                audioManager.Play(Sounds.hit);
                break;
            case EntityType.enemy:
                break;
        }
        foreach (var collider in Physics.OverlapSphere(transform.position, radius)) {
            var other = collider.gameObject.GetComponent<Combat>();
            // make sure that other is not null or this object or of the same type
            if (other == null || other == this || other.entityType == entityType) continue;
            other.TakeDamage();
        }
    }

    /// take damage based on attaking object type
    public void TakeDamage() {
        if (!CheckTakeDamage()) return;
        currentHealth -= hitDamage;
        switch (entityType) {
            case EntityType.player:
                GetComponent<MeshRenderer>().material.color = Color.white * (currentHealth / maxHealth);
                break;
            case EntityType.enemy:
                GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
                break;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}