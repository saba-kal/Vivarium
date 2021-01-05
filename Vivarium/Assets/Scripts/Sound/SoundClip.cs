using UnityEngine;
using System;

[Serializable]
public class SoundClip
{
    public string Name;

    public AudioClip Clip;

    [Range(0f, 1f)]
    public float Volume = 1f;

    [Range(0.1f, 3f)]
    public float Pitch = 1f;

    public bool PlayOnAwake = false;

    public bool Loop = false;
}
