using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public static class AddReceiverToAllAnimators {
    [MenuItem("Tools/Add AnimationEventReceiver To All Animators (Scenes+Prefabs)")]
    public static void Run() {
        int added = 0;

        // Scenes
        for (int s = 0; s < SceneManager.sceneCount; s++) {
            var scene = SceneManager.GetSceneAt(s);
            if (!scene.isLoaded) continue;
            var roots = scene.GetRootGameObjects();
            foreach (var root in roots) {
                var animators = root.GetComponentsInChildren<Animator>(true);
                foreach (var a in animators) {
                    var go = a.gameObject;
                    if (go.GetComponent<SA.AnimationEventReceiver>() == null) {
                        Undo.AddComponent<SA.AnimationEventReceiver>(go);
                        added++;
                        EditorUtility.SetDirty(go);
                    }
                }
            }
            EditorSceneManager.MarkSceneDirty(scene);
        }

        // Prefabs
        string[] guids = AssetDatabase.FindAssets("t:prefab");
        foreach (var g in guids) {
            string path = AssetDatabase.GUIDToAssetPath(g);
            var root = PrefabUtility.LoadPrefabContents(path);
            bool dirty = false;
            var animators = root.GetComponentsInChildren<Animator>(true);
            foreach (var a in animators) {
                var go = a.gameObject;
                if (go.GetComponent<SA.AnimationEventReceiver>() == null) {
                    go.AddComponent(typeof(SA.AnimationEventReceiver));
                    dirty = true;
                    added++;
                }
            }
            if (dirty) PrefabUtility.SaveAsPrefabAsset(root, path);
            PrefabUtility.UnloadPrefabContents(root);
        }

        EditorUtility.DisplayDialog("Add Receiver", $"Added AnimationEventReceiver to {added} GameObjects (Scenes+Prefabs).", "OK");
    }
}
