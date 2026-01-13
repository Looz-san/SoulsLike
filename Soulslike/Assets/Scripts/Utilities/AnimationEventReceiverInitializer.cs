using System.Collections;
using UnityEngine;

namespace SA {
    // Ensures AnimationEventReceiver is present on objects named like "boxMan" at runtime.
    public class AnimationEventReceiverInitializer : MonoBehaviour {
        static AnimationEventReceiverInitializer _instance;

        void Awake() {
            if (_instance != null) {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
            StartCoroutine(EnsureReceivers());
        }

        IEnumerator EnsureReceivers() {
            while (true) {
                AddReceiversToExisting();
                yield return new WaitForSeconds(1f);
            }
        }

        void AddReceiversToExisting() {
            var all = GameObject.FindObjectsOfType<Transform>(true);
            for (int i = 0; i < all.Length; i++) {
                var go = all[i].gameObject;
                if (go.GetComponent<SA.AnimationEventReceiver>() != null) continue;
                string n = go.name.ToLower();
                if (n.Contains("boxman") || n.Contains("box_man") || n.Contains("box-man")) {
                    go.AddComponent<SA.AnimationEventReceiver>();
                }
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void InitOnLoad() {
            var root = new GameObject("__AnimationEventReceiverInitializer");
            root.AddComponent<AnimationEventReceiverInitializer>();
        }
    }
}
