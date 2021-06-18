using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound {
    public string name;
    public AudioClip clip;

    [Range(0, 1f)]
    public float volume = 1;
    [Range(-3f, 3f)]
    public float pitch = 1;
    public bool loop = false;
    public bool playOnAwake = false;

    public AudioSource source;

    /// assigns this sound's values to the AudioSource component
    public void Initialize(AudioSource source) {
        source.volume = volume;
        source.pitch = pitch;
        source.loop = loop;
        source.clip = clip;
        source.playOnAwake = playOnAwake;
        this.source = source;
    }

}