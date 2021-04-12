using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

/// <summary>
/// Custom unity editor for <see cref="LevelGenerator"/>.
/// </summary>
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
