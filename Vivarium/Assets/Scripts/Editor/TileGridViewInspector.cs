using UnityEngine;
using UnityEditor;

/// <summary>
/// Custom unity editor for <see cref="TileGridView"/>.
/// </summary>
[CustomEditor(typeof(TileGridView))]
public class TileGridViewInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var gridView = (TileGridView)target;

        if (GUILayout.Button("Destroy Grid Mesh"))
        {
            gridView.DestroyGridMesh();
        }

        if (GUILayout.Button("Generate Grid Mesh"))
        {
            gridView.CreateGridMesh();
        }
    }
}