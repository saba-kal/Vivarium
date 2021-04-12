using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Assigns a button clicking sound to each of the buttons in the game
/// </summary>
public class AssignAllButtonSounds : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        var buttons = Resources.FindObjectsOfTypeAll(typeof(Button)) as Button[];
        foreach (var button in buttons)
        {
            button.onClick.AddListener(() =>
            {
                SoundManager.GetInstance().Play(Constants.BUTTON_CLICK_SOUND);
            });
        }
    }
}
