using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        PlayerMove,
        PlayerJump,
        TargetHit,
        BonusTime,
        GameOver
    }

    private static Dictionary<Sound, float> soundTimerDictionary;

    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PlayerMove] = 0;
    }

    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(GetAudioClip(sound));
        }
    }

    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
            case Sound.PlayerMove:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimeMax = 0.1f;
                    if (lastTimePlayed + playerMoveTimeMax < Time.time)
                    {
                        return true;
                    }
                    else
                    {
                        return false;

                    }
                }
                else
                {
                    return true;
                }
                //break;
        }
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAudioClip soundClip in GameManager.Instance.soundAudioClipsArray)
        {
            if(soundClip.sound == sound)
            {
                return soundClip.clip;
            }
        }
        Debug.LogError("Sound" + sound + "not found!");
        return null;
    }
}
