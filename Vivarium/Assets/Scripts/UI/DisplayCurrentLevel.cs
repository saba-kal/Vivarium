using UnityEngine;
using System.Collections;
using TMPro;

public class DisplayCurrentLevel : MonoBehaviour
{
    public TextMeshProUGUI LevelDisplayText;

    // Update is called once per frame
    void Update()
    {
        if (PlayerData.CurrentLevelIndex == 0)
        {
            LevelDisplayText.text = $"Level: Tutorial";
        }
        else
        {
            LevelDisplayText.text = $"Level: {PlayerData.CurrentLevelIndex}";
        }
    }
}
