using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditScreen;
    [SerializeField] private GameObject exitingScreen;

    [SerializeField] private GameObject orderPanel;
    [SerializeField] private Text statusText;
    [SerializeField] private Image teaCupImage;

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

    public void UpdateStatusText(string message)
    {
        if (statusText != null)
        {
            statusText.text = message;
        }
        else
        {
            Debug.LogWarning("statusText is not assigned in UIManager!");
        }
    }

    public void UpdateTeaImage(Sprite teaSprite)
    {
        if (teaCupImage != null)
        {
            teaCupImage.sprite = teaSprite;
        }
        else
        {
            Debug.LogWarning("teaCupImage is not assigned in UIManager!");
        }
    }
}
