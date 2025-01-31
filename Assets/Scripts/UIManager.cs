using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditScreen;
    [SerializeField] private GameObject exitingScreen;

    [SerializeField] private GameObject teaBrewingPanel;
    [SerializeField] private TMP_Text statusText;
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

    public void ShowTeaBrewingPanel(string teaOrder)
    {
        if (teaBrewingPanel != null)
        {
            teaBrewingPanel.SetActive(true);
        }

        if (statusText != null)
        {
            statusText.text = "Order: " + teaOrder;
        }
    }

    public void HideTeaBrewingPanel()
    {
        if (teaBrewingPanel != null)
        {
            teaBrewingPanel.SetActive(false);
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
