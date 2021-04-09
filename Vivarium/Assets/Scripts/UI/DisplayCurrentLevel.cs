using UnityEngine;
using System.Collections;
using TMPro;

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
