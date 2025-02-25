using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject homeConfirmationScreen;
    private int currLevel;
    private int nextLevel;

    private void Start()
    {
        if (pauseScreen != null) pauseScreen.SetActive(false);
        if (homeConfirmationScreen != null) homeConfirmationScreen.SetActive(false);
    }

    public void ShowPauseScreen()
    {
        AudioManagers.Instance.PlaySFX("dink");
        if (pauseScreen != null)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Debug.LogWarning("pauseScreen is not assigned in UIManager!");
        }
    }

    //retry day button
    public void RetryButton()
    {
        AudioManagers.Instance.PlaySFX("dink");
        Debug.Log("Retry day...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void ResumeButton()
    {
        AudioManagers.Instance.PlaySFX("dink");
        if (pauseScreen != null)
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            Debug.LogWarning("pauseScreen is not assigned in UIManager!");
        }
        
    }

    public void ShowHomeConfirmation()
    {
        AudioManagers.Instance.PlaySFX("dink");
        if (homeConfirmationScreen != null)
        {
            homeConfirmationScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Debug.LogWarning("homeConfirmationScreen is not assigned in UIManager!");
        }
    }

    public void CancelGoHome()
    {
        AudioManagers.Instance.PlaySFX("dink");
        if (homeConfirmationScreen != null)
        {
            homeConfirmationScreen.SetActive(false);
            ResumeButton();
        }
        else
        {
            Debug.LogWarning("homeConfirmationScreen is not assigned in UIManager!");
        }
    }
    
    public void GoHomeButton()
    {
        AudioManagers.Instance.PlaySFX("dink");
        Debug.Log("Back to Main Menu...");
        SavingManager.Instance.LoadGameData();
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    
}


            