using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager audioManager;

    public Sound[] sounds;
    System.Random random = new System.Random();

    private void Awake() {
        audioManager = this;
        Array.ForEach<Sound>(sounds, s => s.Initialize(gameObject.AddComponent<AudioSource>()));
    }

    void Start() {
        var onAwakeSounds = Array.FindAll<Sound>(sounds, s => s.playOnAwake);
        Array.ForEach<Sound>(onAwakeSounds, s => Play(s.name));
    }

    public void Play(string name) {
        var sound = Array.Find<Sound>(sounds, s => s.name == name);
        if (sound == null) {
            Debug.LogWarning("sound " + name + " not found");
            return;
        }
        
        sound.source.Play();
    }

    public void Play(Sounds sounds) {
        Play(GetName(sounds));
    }

    public string GetName(Sounds sound) {
        switch (sound) {
            case Sounds.enemyNotice:
                return "enemyNotice";
            case Sounds.pickup:
                return "pickup";
            case Sounds.getHurt:
                return "grunt";
            case Sounds.footSteps:
                return RandomChoice("footSteps-1", "footSteps-2", "footSteps-3", "footSteps-4", "footSteps-5");
            case Sounds.hit:
                return RandomChoice("hit-1", "hit-2", "hit-3");
            case Sounds.swordSwing:
                return RandomChoice("sword-1", "sword-2", "sword-3", "sword-4");
            case Sounds.pig:
                return RandomChoice("pig-1", "pig-2", "pig-3", "pig-4", "pig-5");
            default:
                return "";
        }
    }
    string RandomChoice(params string[] list) {
        return list[random.Next(list.Length)];
    }
}

public enum Sounds {
    footSteps,
    hit,
    getHurt,
    pig,
    swordSwing,
    enemyNotice,
    pickup,
}