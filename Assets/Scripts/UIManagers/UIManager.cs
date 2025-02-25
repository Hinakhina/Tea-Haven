using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditScreen;
    [SerializeField] private GameObject exitingScreen;

    private void Start()
    {
        if (mainMenu != null) mainMenu.SetActive(true);
        if (creditScreen != null) creditScreen.SetActive(false);
        if (exitingScreen != null) exitingScreen.SetActive(false);
    }

    public void ShowCredits()
    {
        if (creditScreen != null)
        {
            creditScreen.SetActive(true);
        }
        else
        {
            Debug.LogWarning("creditScreen is not assigned in UIManager!");
        }
    }

    public void CloseCredits()
    {
        if (creditScreen != null)
        {
            creditScreen.SetActive(false);
        }
        else
        {
            Debug.LogWarning("creditScreen is not assigned in UIManager!");
        }
    }

    public void ShowExitConfirmation()
    {
        if (exitingScreen != null)
        {
            exitingScreen.SetActive(true);
        }
        else
        {
            Debug.LogWarning("exitingScreen is not assigned in UIManager!");
        }
    }

    public void CancelExit()
    {
        if (exitingScreen != null)
        {
            exitingScreen.SetActive(false);
        }
        else
        {
            Debug.LogWarning("exitingScreen is not assigned in UIManager!");
        }
    }
}