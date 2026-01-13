using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public static class FixAnimationEventReceivers {
    [MenuItem("Tools/Fix AnimationEvent Receivers (boxMan)")]
    public static void FixReceivers() {
        int added = 0;

        // Scan open scenes
        for (int s = 0; s < SceneManager.sceneCount; s++) {
            var scene = SceneManager.GetSceneAt(s);
            if (!scene.isLoaded) continue;
            var roots = scene.GetRootGameObjects();
            foreach (var root in roots) {
                var targets = root.GetComponentsInChildren<Animator>(true);
                foreach (var a in targets) {
                    if (a.gameObject.name.ToLower().Contains("boxman")) {
                        if (a.gameObject.GetComponent<SA.AnimationEventReceiver>() == null) {
                            Undo.AddComponent<SA.AnimationEventReceiver>(a.gameObject);
                            added++;
                        }
                    }
                }
            }
        }

        // Scan prefabs named boxMan
        string[] guids = AssetDatabase.FindAssets("boxMan t:prefab");
        foreach (var g in guids) {
            string path = AssetDatabase.GUIDToAssetPath(g);
            var root = PrefabUtility.LoadPrefabContents(path);
            bool dirty = false;
            var anims = root.GetComponentsInChildren<Animator>(true);
            foreach (var a in anims) {
                if (a.gameObject.name.ToLower().Contains("boxman")) {
                    if (a.gameObject.GetComponent<SA.AnimationEventReceiver>() == null) {
                        a.gameObject.AddComponent(typeof(SA.AnimationEventReceiver));
                        dirty = true;
                        added++;
                    }
                }
            }
            if (dirty) PrefabUtility.SaveAsPrefabAsset(root, path);
            PrefabUtility.UnloadPrefabContents(root);
        }

        EditorUtility.DisplayDialog("Fix AnimationEvent Receivers", $"Added receiver to {added} GameObjects (search: 'boxMan').\nIf warning persists, attach 'AnimationEventReceiver' to the animated GameObject in the prefab or scene.", "OK");
    }
}
