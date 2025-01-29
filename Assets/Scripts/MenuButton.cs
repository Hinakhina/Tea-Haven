using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class MenuButton : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] GameObject creditScreen; // No longer used directly
    [SerializeField] GameObject exitingScreen; // No longer used directly

    private void Start()
    {
        // Find the GameManager in the scene
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }
    }

    public void NewGameButton()
    {
        // Calls GameManager's StartNewGame method
        gameManager?.StartNewGame();
    }

    public void ContinueButton()
    {
        // Calls GameManager's ContinueGame method
        gameManager?.ContinueGame();
    }

    public void SettingsButton()
    {
        // Calls GameManager's OpenSettings method
        gameManager?.OpenSettings();
    }

    public void CreditsButton()
    {
        // Calls GameManager's ShowCredits method
        gameManager?.ShowCredits();
    }

    public void CloseCreditsButton()
    {
        // Calls GameManager's CloseCredits method
        gameManager?.CloseCredits();
    }

    public void ExitButton()
    {
        // Calls GameManager's ShowExitConfirmation method
        gameManager?.ShowExitConfirmation();
    }

    public void NoExitButton()
    {
        // Calls GameManager's CancelExit method
        gameManager?.CancelExit();
    }

    public void YesExitButton()
    {
        // Calls GameManager's ExitGame method
        gameManager?.ExitGame();
    }
}