using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
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

            if (sound.PlayOnAwake)
            {
                audioSource.Play();
            }

            _soundBank[sound.Name] = audioSource;
        }
    }

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

    public static SoundManager GetInstance()
    {
        return FindObjectOfType<SoundManager>();
    }

    public void SetVolume(float volumeFloat)
    {
        foreach (var source in _soundBank.Values)
        {
            source.volume = volumeFloat;
        }
    }
}
