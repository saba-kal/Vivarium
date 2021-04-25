using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Controls volume, playing/pausing, and other settings for the sounds in the game
/// </summary>
public class SoundManager : MonoBehaviour
{
    public AudioMixer AudioMixer;
    public List<SoundClip> Sounds;

    private Dictionary<string, AudioSource> _soundBank;

    // Start is called before the first frame update
    void Awake()
    {
        _soundBank = new Dictionary<string, AudioSource>();
        foreach (var sound in Sounds)
        {
            var audioSourceGameObject = new GameObject(sound.Name);
            audioSourceGameObject.transform.SetParent(transform);

            var audioSource = audioSourceGameObject.AddComponent<AudioSource>();
            audioSource.clip = sound.Clip;
            audioSource.volume = sound.Volume;
            audioSource.pitch = sound.Pitch;
            audioSource.loop = sound.Loop;
            audioSource.outputAudioMixerGroup = sound.AudioMixerGroup;

            if (sound.PlayOnAwake)
            {
                audioSource.Play();
            }

            _soundBank[sound.Name] = audioSource;
        }
    }

    /// <summary>
    /// Plays a sound clip
    /// </summary>
    /// <param name="soundName">Name of a sound clip</param>
    public void Play(string soundName)
    {
        if (_soundBank.ContainsKey(soundName))
        {
            _soundBank[soundName].Play();
        }
        else
        {
            Debug.LogError($"Unable to find sound clip named {soundName}");
        }
    }

    /// <summary>
    /// Stops a sound clip from playing
    /// </summary>
    /// <param name="soundName">Name of a sound clip</param>
    public void Stop(string soundName)
    {
        if (_soundBank.ContainsKey(soundName))
        {
            _soundBank[soundName].Stop();
        }
        else
        {
            Debug.LogError($"Unable to find sound clip named {soundName}");
        }
    }

    /// <summary>
    /// Pauses a sound clip
    /// </summary>
    /// <param name="soundName"></param>
    public void Pause(string soundName)
    {
        if (_soundBank.ContainsKey(soundName))
        {
            _soundBank[soundName].Pause();
        }
        else
        {
            Debug.LogError($"Unable to find sound clip named {soundName}");
        }
    }

    /// <summary>
    /// Resumes a paused sound clip
    /// </summary>
    /// <param name="soundName">Name of a sound clip</param>
    public void Resume(string soundName)
    {
        if (_soundBank.ContainsKey(soundName))
        {
            _soundBank[soundName].UnPause();
        }
        else
        {
            Debug.LogError($"Unable to find sound clip named {soundName}");
        }
    }

    /// <summary>
    /// Returns a sound manager
    /// </summary>
    /// <returns></returns>
    public static SoundManager GetInstance()
    {
        return FindObjectOfType<SoundManager>();
    }

    /// <summary>
    /// Changes the volume of the sound clip
    /// </summary>
    /// <param name="volumeFloat">The volume of a sound clip</param>
    public void SetVolume(float volumeFloat)
    {
        AudioMixer.SetFloat("MasterVolume", volumeFloat);
    }
}
