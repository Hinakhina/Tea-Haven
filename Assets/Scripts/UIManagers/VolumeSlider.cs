using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VolumeSlider : MonoBehaviour
{
    public Slider generalVolumeSlider, musicVolumeSlider, sfxVolumeSlider;
    private bool generalChanged, musicChanged, sfxChanged;

    private void Start()
    {
        LoadAndInitializeSliders();
        AddEventTriggers(generalVolumeSlider);
        AddEventTriggers(musicVolumeSlider);
        AddEventTriggers(sfxVolumeSlider);
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

    private void AddEventTriggers(Slider slider)
    {
        if (slider == null) return;

        EventTrigger trigger = slider.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerUp  // Detect when slider handle is released
        };
        entry.callback.AddListener((data) => { OnSliderReleased(); });
        trigger.triggers.Add(entry);
    }

    public void SetGeneralVolume(float volume)
    {
        generalChanged = true;
        AudioManagers.Instance.SetGeneralVolume(volume);
        PlayerPrefs.SetFloat("GeneralVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicChanged = true;
        AudioManagers.Instance.SetMusicVolume(volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxChanged = true;
        AudioManagers.Instance.SetSFXVolume(volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void OnSliderReleased()
    {
        if (generalChanged || musicChanged || sfxChanged)
        {
            AudioManagers.Instance.PlaySFX("thud");
            generalChanged = false;
            musicChanged = false;
            sfxChanged = false;
        }
    }
}
