using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Main Menu UI
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditScreen;
    [SerializeField] private GameObject exitingScreen;

    // Tea Brewing UI
    [SerializeField] private GameObject brewingMachine;
    [SerializeField] private Image teaCupImage;
    [SerializeField] private GameObject orderPanel;
    [SerializeField] private Text statusText;

    private bool isBrewing = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        if (mainMenu != null) mainMenu.SetActive(true);
        if (creditScreen != null) creditScreen.SetActive(false);
        if (exitingScreen != null) exitingScreen.SetActive(false);
    }

    // ------------------ MAIN MENU FUNCTIONS ------------------

    public void StartNewGame()
    {
        Debug.Log("Starting a new game...");
        SceneManager.LoadScene("GamePlay"); // Load the game scene
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

    // ------------------ TEA BREWING GAME FUNCTIONS ------------------

    public void SelectTeaLeaves()
    {
        Debug.Log("Tea leaves selected!");
        statusText.text = "Tea leaves selected!";
    }

    public void StartBrewing()
    {
        if (!isBrewing)
        {
            isBrewing = true;
            statusText.text = "Brewing...";
            StartCoroutine(BrewingProcess());
        }
    }

    private IEnumerator BrewingProcess()
    {
        yield return new WaitForSeconds(3);
        statusText.text = "Brewing complete!";
        isBrewing = false;
    }

    public void ServeTea()
    {
        if (!isBrewing)
        {
            Debug.Log("Tea served!");
            statusText.text = "Tea served!";
        }
    }
}
