using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider generalVolumeSlider, musicVolumeSlider, sfxVolumeSlider;

    private void Start()
    {
        LoadAndInitializeSliders();
    }

    private void LoadAndInitializeSliders()
    {
        float generalVolume = PlayerPrefs.GetFloat("GeneralVolume", 1.0f);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        AudioManagers.Instance.SetGeneralVolume(generalVolume);
        AudioManagers.Instance.SetMusicVolume(musicVolume);
        AudioManagers.Instance.SetSFXVolume(sfxVolume);

        if (generalVolumeSlider != null)
        {
            generalVolumeSlider.value = generalVolume;
            generalVolumeSlider.onValueChanged.AddListener(SetGeneralVolume);
        }

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = musicVolume;
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = sfxVolume;
            sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        }
    }

    public void SetGeneralVolume(float volume)
    {
        AudioManagers.Instance.PlaySFX("chunk");
        AudioManagers.Instance.SetGeneralVolume(volume);
        PlayerPrefs.SetFloat("GeneralVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        AudioManagers.Instance.PlaySFX("chunk");
        AudioManagers.Instance.SetMusicVolume(volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        AudioManagers.Instance.PlaySFX("chunk");
        AudioManagers.Instance.SetSFXVolume(volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
}
