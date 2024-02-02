using UnityEngine;
using UnityEditor;

public class GenerateLoops : EditorWindow
{
    GameObject loopPrefab;

    [MenuItem("Window/Generate Loops")]
    static void Init()
    {
        GenerateLoops window = (GenerateLoops)EditorWindow.GetWindow(typeof(GenerateLoops));
        window.Show();
    }

    void OnGUI()
    {
        loopPrefab = (GameObject)EditorGUILayout.ObjectField("Loop Prefab", loopPrefab, typeof(GameObject), true);

        if (GUILayout.Button("Build"))
        {
            if (loopPrefab == null)
                return;

            GameObject loops = new GameObject();

            for (int i = 0; i < 500; i++)
            {
                Vector3 newPos = new Vector3(Random.Range(-1000f, 1000f), Random.Range(0f,100f), Random.Range(-1000f, 1000f));
                Quaternion newRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
                GameObject newLoop = GameObject.Instantiate(loopPrefab, newPos, newRotation, loops.transform);
            }
        }
    }
}