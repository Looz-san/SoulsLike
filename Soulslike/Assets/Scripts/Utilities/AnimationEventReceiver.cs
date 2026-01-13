using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA {
    public class AnimationEventReceiver : MonoBehaviour {
        public void OpenDamageColliders() {
            var es = GetComponent<EnemyStats>() ?? GetComponentInParent<EnemyStats>();
            if (es != null) es.OpenDamageColliders();
            else Debug.LogWarning("OpenDamageColliders called but no EnemyStats found on " + gameObject.name, gameObject);
        }

        public void CloseDamageColliders() {
            var es = GetComponent<EnemyStats>() ?? GetComponentInParent<EnemyStats>();
            if (es != null) es.CloseDamageColliders();
            else Debug.LogWarning("CloseDamageColliders called but no EnemyStats found on " + gameObject.name, gameObject);
        }
    }
}
