using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManagers : MonoBehaviour
{
    public static AudioManagers Instance;

    public Sound[] musicSounds, sfxSounds, loopSounds;
    public AudioSource musicSource, sfxSource, loopSource;

    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";
    private const string LOOP_VOLUME_KEY = "LoopVolume";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadVolumeSettings();
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadVolumeSettings();
    }

    private void Start()
    {
        PlayMusic("Menu");
        //Theme will play once the game starts. if not needed, delete Start().

    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x=> x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
            musicSource.Stop();
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x=> x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void PlayLoop(string name)
    {
        Sound s = Array.Find(loopSounds, x=> x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            loopSource.clip = s.clip;
            loopSource.Play();
        }
    }

    private void LoadVolumeSettings()
    {
        musicSource.volume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 0.5f);
        sfxSource.volume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 0.5f);
        loopSource.volume = PlayerPrefs.GetFloat(LOOP_VOLUME_KEY, 0.5f);
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
    }

     public void FXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
    }
     public void LoopVolume(float volume)
    {
        loopSource.volume = volume;
        PlayerPrefs.SetFloat(LOOP_VOLUME_KEY, volume);
    }
}
