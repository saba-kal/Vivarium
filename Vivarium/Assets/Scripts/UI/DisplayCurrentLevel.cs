using UnityEngine;
using System.Collections;
using TMPro;

public class DisplayCurrentLevel : MonoBehaviour
{
    public TextMeshProUGUI LevelDisplayText;

    // Update is called once per frame
    void Update()
    {
        LevelDisplayText.text = $"Level: {PlayerData.CurrentLevelIndex + 1}";
    }
}
