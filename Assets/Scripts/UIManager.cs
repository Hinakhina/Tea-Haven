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
        creditScreen.SetActive(true);
    }

    public void CloseCredits()
    {
        creditScreen.SetActive(false);
    }

    public void ShowExitConfirmation()
    {
        exitingScreen.SetActive(true);
    }

    public void CancelExit()
    {
        exitingScreen.SetActive(false);
    }

    public void UpdateStatusText(string message)
    {
        if (statusText != null)
        {
            statusText.text = message;
        }
    }

    public void UpdateTeaImage(Sprite teaSprite)
    {
        if (teaCupImage != null)
        {
            teaCupImage.sprite = teaSprite;
        }
    }
}
