﻿using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(InventoryItemsView))]
public class InventoryItemsViewInspector : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var inventoryItemsView = (InventoryItemsView)target;

        if (GUILayout.Button("Preview Inventory"))
        {
            inventoryItemsView.PreviewInventory();
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
    }
}