using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public SoundAudioClip[] sounds;
    private static Dictionary<string, float> soundTimerDictionary;

    public static SoundManager Instance { get { return _instance; } }

    public enum Sound
    {
        PlayerMove,
        PlayerJump,
        TargetHit,
        BonusTime,
        GameOver
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        soundTimerDictionary = new Dictionary<string, float>();

        foreach (SoundAudioClip sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.isLoop;

            if (sound.hasCooldown)
            {
                Debug.Log(sound.name);
                soundTimerDictionary[sound.name] = sound.cooldown;
            }
        }
    }

    public void Play(string name)
    {
        SoundAudioClip sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            return;
        }

        if (!CanPlaySound(sound)) return;

        sound.source.Play();
    }

    public void Stop(string name)
    {
        SoundAudioClip sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            return;
        }

        sound.source.Stop();
    }

    //public static void PlaySound(Sound sound)
    //{
    //    if (CanPlaySound(sound))
    //    {
    //        GameObject soundGameObject = new GameObject("Sound");
    //        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
    //        audioSource.PlayOneShot(GetAudioClip(sound));
    //    }
    //}

    private static bool CanPlaySound(SoundAudioClip sound)
    {
        if (soundTimerDictionary.ContainsKey(sound.name))
        {
            float lastTimePlayed = soundTimerDictionary[sound.name];

            if (lastTimePlayed + sound.cooldown < Time.time)
            {
                soundTimerDictionary[sound.name] = Time.time;
                return true;
            }

            return false;
        }

        return true;
    }

    //private static AudioClip GetAudioClip(Sound sound)
    //{
    //    foreach (SoundAudioClip soundClip in GameManager.Instance.soundAudioClipsArray)
    //    {
    //        if(soundClip.sound == sound)
    //        {
    //            return soundClip.clip;
    //        }
    //    }
    //    Debug.LogError("Sound" + sound + "not found!");
    //    return null;
    //}
}
