using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Controls the sound based on the volume slider UI bar.
/// </summary>
public class VolumeSlider : MonoBehaviour
{
    public Slider VolumeSliderElem;

    private SoundManager _soundManager;
    private static float _volume = 0.5f;

    void Start()
    {
        _soundManager = SoundManager.GetInstance();
        if (_soundManager == null)
        {
            Debug.LogError("Unable to find instance of sound manager to set volume level.");
            return;
        }

        VolumeSliderElem.value = _volume;
        VolumeSliderElem.onValueChanged.AddListener(ChangeVolume);
        _soundManager.SetVolume(VolumeSliderElem.value);
    }

    private void ChangeVolume(float value)
    {
        _soundManager.SetVolume(value);
        _volume = value;
    }
}
