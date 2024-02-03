using UnityEngine;
using UnityEditor;

public class GenerateLoops : EditorWindow
{
    GameObject loopPrefab;

    [MenuItem("Window/Generate Loops")]
    static void Init()
    {
        var window = (GenerateLoops)GetWindow(typeof(GenerateLoops));
        window.Show();
    }

    void OnGUI()
    {
        loopPrefab = (GameObject)EditorGUILayout.ObjectField("Loop Prefab", loopPrefab, typeof(GameObject), true);

        if (GUILayout.Button("Build"))
        {
            if (loopPrefab == null)
                return;

            var loops = new GameObject("Loops");
            for (int i = 0; i < 500; i++)
            {
                var newPos = new Vector3(Random.Range(-1000f, 1000f), Random.Range(0f,100f), Random.Range(-1000f, 1000f));
                var newRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
                // instantiate the prefab
                var loop = (GameObject)PrefabUtility.InstantiatePrefab(loopPrefab);
                loop.transform.position = newPos;
                loop.transform.rotation = newRotation;
                loop.transform.parent = loops.transform;
            }
        }
    }
}