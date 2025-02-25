using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayButton : MonoBehaviour
{
    [SerializeField] GameObject helpPanel;

    private void Start()
    {
        if (helpPanel != null) helpPanel.SetActive(false);
    }

    public void ShowHelpPanel()
    {
        AudioManagers.Instance.PlaySFX("dink");
        if (helpPanel != null)
        {
            helpPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Debug.LogWarning("helpPanel is not assigned in UIManager!");
        }
    }

    public void ResumeButton()
    {
        AudioManagers.Instance.PlaySFX("dink");
        if (helpPanel != null)
        {
            helpPanel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            Debug.LogWarning("helpPanel is not assigned in UIManager!");
        }
        
    }
    
}


            