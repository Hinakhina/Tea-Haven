using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayButton : MonoBehaviour
{
    [SerializeField] GameObject helpPanel;
    [SerializeField] GameObject page1, page2;

    private void Start()
    {
        if (helpPanel != null){
            helpPanel.SetActive(false);
        }
    }

    public void ShowHelpPanel()
    {
        AudioManagers.Instance.PlaySFX("openBook");
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
        AudioManagers.Instance.PlaySFX("closeBook");
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

    public void nextPage(){
        AudioManagers.Instance.PlaySFX("flips");
        page1.SetActive(false);
        page2.SetActive(true);
    }

    public void prevPage(){
        AudioManagers.Instance.PlaySFX("flips");
        page2.SetActive(false);
        page1.SetActive(true);
    }
    
}


            