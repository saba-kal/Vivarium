using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(TileGridController))]
public class TileGridControllerInspector : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var gridController = (TileGridController)target;

        if (GUILayout.Button("Clear Grid Data"))
        {
            gridController.ClearGridData();
        }

        if (GUILayout.Button("Generate Grid Data"))
        {
            gridController.GenerateGridData();
        }
    }
}
