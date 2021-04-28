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
        GraphicsDropdown.RefreshShownValue();
    }

    private void SetupFullsceeenToggle()
    {
        FullscreenToggle.onValueChanged.AddListener((isFullscreen) =>
        {
            Screen.fullScreen = isFullscreen;
        });
        FullscreenToggle.isOn = Screen.fullScreen;
    }

    private void SetupResolutionDropdown()
    {
        ResolutionDropdown.ClearOptions();

        var resolutionOptions = new List<string>();

        var currentResolutionIndex = 0;
        var resIndex = 0;
        var addedResolutions = new List<(int, int)>();

        foreach (var resolution in Screen.resolutions)
        {
            if (addedResolutions.Contains((resolution.width, resolution.height)))
            {
                continue;
            }
            addedResolutions.Add((resolution.width, resolution.height));

            resolutionOptions.Add($"{resolution.width} x {resolution.height}");

            if (resolution.width == Screen.width &&
                resolution.height == Screen.height)
            {
                currentResolutionIndex = resIndex;
            }

            resIndex++;
        }

        ResolutionDropdown.AddOptions(resolutionOptions);
        ResolutionDropdown.value = currentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();

        ResolutionDropdown.onValueChanged.AddListener((resolutionIndex) =>
        {
            var newResolution = addedResolutions[resolutionIndex];
            Screen.SetResolution(newResolution.Item1, newResolution.Item2, Screen.fullScreen);
        });
    }
}
