using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

[CustomEditor(typeof(GridPointCalculator))]
public class GridPointCalculatorInspector : Editor
{
    private int _selectedCharacter = 0;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (!Application.isPlaying)
        {
            return;
        }

        var characterDropdownOptions = new List<string> { "None" };
        var aiCharacters = TurnSystemManager.Instance?.AIManager?.AICharacters;
        if (aiCharacters != null)
        {
            foreach (var aiCharacter in aiCharacters)
            {
                characterDropdownOptions.Add($"{aiCharacter.Character.Name}");
            }
        }
        _selectedCharacter = EditorGUILayout.Popup("Character to Calculate", _selectedCharacter, characterDropdownOptions.ToArray());

        var gridPointCalculator = (GridPointCalculator)target;

        if (GUILayout.Button("Calculate"))
        {
            if (_selectedCharacter > 0)
            {
                var aiCharacter = aiCharacters[_selectedCharacter - 1];
                gridPointCalculator.CalculateGridPoints(aiCharacter);
                gridPointCalculator.PreviewGridPoints();
            }
            else
            {
                Debug.LogWarning("Please select a character from the dropdown.");
            }
        }
    }
}