using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
