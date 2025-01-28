using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditScreen;
    [SerializeField] private GameObject exitingScreen;

    private void Start()
    {
        if (mainMenu != null) mainMenu.SetActive(true);
        if (creditScreen != null) creditScreen.SetActive(false);
        if (exitingScreen != null) exitingScreen.SetActive(false);
    }

    public void StartNewGame()
    {
        Debug.Log("Starting a new game...");
        SceneManager.LoadScene("GamePlay");
    }

    public void ContinueGame()
    {
        Debug.Log("Continuing the game...");
        SceneManager.LoadScene("GamePlay");
    }

    public void OpenSettings()
    {
        Debug.Log("Opening settings...");
        SceneManager.LoadScene("SettingsPage");
    }

    public void ShowCredits()
    {
        if (creditScreen != null)
        {
            Debug.Log("Showing credits...");
            creditScreen.SetActive(true);
        }
    }

    public void CloseCredits()
    {
        if (creditScreen != null)
        {
            Debug.Log("Closing credits...");
            creditScreen.SetActive(false);
        }
    }

    public void ShowExitConfirmation()
    {
        if (exitingScreen != null)
        {
            Debug.Log("Showing exit confirmation...");
            exitingScreen.SetActive(true);
        }
    }

    public void CancelExit()
    {
        if (exitingScreen != null)
        {
            Debug.Log("Cancel exit...");
            exitingScreen.SetActive(false);
        }
    }

    public void ExitGame()
    {
        Debug.Log("Exiting the game...");
        Application.Quit();
    }
}
