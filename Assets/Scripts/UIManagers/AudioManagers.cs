using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManagers : MonoBehaviour
{
    public static AudioManagers Instance;

    public Sound[] musicSounds, sfxSounds, loopSounds;
    public AudioSource musicSource, sfxSource, loopSource;

    private const string GENERAL_VOLUME_KEY = "GeneralVolume";
    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
            return;
        }

        musicSource.clip = s.clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
            return;
        }
        sfxSource.PlayOneShot(s.clip);
    }

    private void LoadVolumeSettings()
    {
        SetGeneralVolume(PlayerPrefs.GetFloat(GENERAL_VOLUME_KEY, 1.0f));
        SetMusicVolume(PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 0.5f));
        SetSFXVolume(PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 0.5f));
    }

    public void SetGeneralVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat(GENERAL_VOLUME_KEY, volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
    }
}