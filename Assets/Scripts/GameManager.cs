using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Main Menu UI
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditScreen;
    [SerializeField] private GameObject exitingScreen;

    // Tea Brewing UI
    [SerializeField] private BrewingMachine brewingMachine;
    [SerializeField] private Image teaCupImage;
    [SerializeField] private GameObject orderPanel;
    [SerializeField] private Text statusText;

    private bool isBrewing = false;
    private string currentOrder;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
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

    // ------------------ TEA BREWING GAME FUNCTIONS ------------------

    public void ReceiveOrder(string teaType)
    {
        currentOrder = teaType;
        statusText.text = "New Order: " + teaType;
    }

    public void SelectTeaLeaves(string teaType)
    {
        Debug.Log("Tea leaves selected: " + teaType);
        statusText.text = "Selected: " + teaType;
        brewingMachine.SetTeaType(teaType);
    }

    public void StartBrewing()
    {
        if (!isBrewing && brewingMachine.HasTea())
        {
            isBrewing = true;
            statusText.text = "Brewing...";
            StartCoroutine(BrewingProcess());
        }
        else
        {
            statusText.text = "Select tea leaves first!";
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
        if (!isBrewing && brewingMachine.HasTea())
        {
            string brewedTea = brewingMachine.GetBrewedTea();
            if (brewedTea == currentOrder)
            {
                Debug.Log("Tea served successfully!");
                statusText.text = "Tea served: " + brewedTea;
            }
            else
            {
                Debug.Log("Wrong order!");
                statusText.text = "Wrong tea!";
            }
            brewingMachine.ClearTea();
        }
    }
}
