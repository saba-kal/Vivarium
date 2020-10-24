using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorInspector : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var levelGenerator = (LevelGenerator)target;

        if (GUILayout.Button("Generate Level"))
        {
            levelGenerator.GenerateLevel();
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
    }
}
