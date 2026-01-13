using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA {
    public class EnemyStats : MonoBehaviour {
        public float maxHealth = 100f;
        public float currentHealth = 100f;

        public void TakeDamage(float damage) {
            currentHealth -= damage;
            if (currentHealth <= 0) {
                Die();
            }
        }

        public void Die() {
            // Animation ตาย หรือ destroy
            Destroy(gameObject);
        }

        public void GloryKill() {
            // Special death for glory kill
            Die();
        }

        // AnimationEvent receivers for opening/closing damage colliders
        public void OpenDamageColliders() {
            ToggleDamageColliders(true);
        }

        public void CloseDamageColliders() {
            ToggleDamageColliders(false);
        }

        void ToggleDamageColliders(bool open) {
            Collider[] cols = GetComponentsInChildren<Collider>(true);
            for (int i = 0; i < cols.Length; i++) {
                var c = cols[i];
                string n = c.gameObject.name.ToLower();
                if (n.Contains("damage") || n.Contains("hurt") || c.isTrigger) {
                    c.enabled = open;
                }
            }
        }
    }
}