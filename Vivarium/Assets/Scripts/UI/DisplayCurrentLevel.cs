using UnityEngine;
using System.Collections;
using TMPro;

/// <summary>
/// Shows the current level number. Displayed in the top right of the UI.
/// </summary>
public class DisplayCurrentLevel : MonoBehaviour
{
    public TextMeshProUGUI LevelDisplayText;

    // Update is called once per frame
    void Update()
    {
        if (TutorialManager.GetIsTutorial())
        {
            LevelDisplayText.text = $"Level: Tutorial";
        }
        else
        {
            LevelDisplayText.text = $"Level: {PlayerData.CurrentLevelIndex}";
        }
    }
}
