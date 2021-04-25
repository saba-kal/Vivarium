using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Handles settings menu logic.
/// </summary>
public class SettingsMenu : MonoBehaviour
{
    public AudioMixer AudioMixer;
    public Slider MasterVolumeSlider;
    public Slider MusicVolumeSlider;
    public Slider SfxVolumeSlider;
    public TMP_Dropdown GraphicsDropdown;
    public TMP_Dropdown ResolutionDropdown;
    public Toggle FullscreenToggle;

    private SoundManager _soundManager;
    private static float _volume = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _soundManager = SoundManager.GetInstance();

        SetupVolumeSlider(MasterVolumeSlider, "MasterVolume");
        SetupVolumeSlider(MusicVolumeSlider, "MusicVolume");
        SetupVolumeSlider(SfxVolumeSlider, "EffectsVolume");
        SetupGraphicsDropdown();
        SetupResolutionDropdown();
        SetupFullsceeenToggle();
    }

    private void SetupVolumeSlider(
        Slider slider,
        string mixerGroup)
    {
        if (AudioMixer.GetFloat(mixerGroup, out var initialValue))
        {
            slider.value = initialValue;
        }
        slider.onValueChanged.AddListener((value) => AudioMixer.SetFloat(mixerGroup, value));
    }

    private void SetupGraphicsDropdown()
    {
        GraphicsDropdown.value = QualitySettings.GetQualityLevel();
        GraphicsDropdown.onValueChanged.AddListener((index) =>
        {
            QualitySettings.SetQualityLevel(index);
        });
    }

    private void SetupFullsceeenToggle()
    {
        FullscreenToggle.onValueChanged.AddListener((isFullscreen) =>
        {
            Screen.fullScreen = isFullscreen;
        });
    }

    private void SetupResolutionDropdown()
    {
        ResolutionDropdown.ClearOptions();

        var resolutionOptions = new List<string>();

        var currentResolutionIndex = 0;
        for (var i = 0; i < Screen.resolutions.Length; i++)
        {
            var resolution = Screen.resolutions[i];
            resolutionOptions.Add($"{resolution.width} x {resolution.height}");

            if (resolution.width == Screen.currentResolution.width &&
                resolution.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        ResolutionDropdown.AddOptions(resolutionOptions);
        ResolutionDropdown.value = currentResolutionIndex;

        ResolutionDropdown.onValueChanged.AddListener((resolutionIndex) =>
        {
            var newResolution = Screen.resolutions[resolutionIndex];
            Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreen);
        });
    }
}
