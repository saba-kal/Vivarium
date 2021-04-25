using UnityEngine;
using System;
using UnityEngine.Audio;

/// <summary>
/// Represents sound clips used
/// </summary>
[Serializable]
public class SoundClip
{
    /// <summary>
    /// Name of the sound clip
    /// </summary>
    public string Name;

    /// <summary>
    /// The audio data that this sound clip references
    /// </summary>
    public AudioClip Clip;

    /// <summary>
    /// The range of the volume
    /// </summary>
    [Range(0f, 1f)]
    public float Volume = 1f;

    /// <summary>
    /// The range of the pitch of the sound clip
    /// </summary>
    [Range(0.1f, 3f)]
    public float Pitch = 1f;

    /// <summary>
    /// A check to see if the sound clip should play when the game is started
    /// </summary>
    public bool PlayOnAwake = false;

    /// <summary>
    /// A check to see if the sound clip should be looped during the game
    /// </summary>
    public bool Loop = false;

    /// <summary>
    /// The audio mixer group this sound clip outputs to.
    /// </summary>
    public AudioMixerGroup AudioMixerGroup;
}
