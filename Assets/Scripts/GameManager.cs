using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public UIManager UIManager { get; private set; }
    public BrewingMachine BrewingMachine { get; private set; }

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

        // Find UIManager and BrewingMachine in the scene
        UIManager = FindObjectOfType<UIManager>();
        BrewingMachine = FindObjectOfType<BrewingMachine>();

        if (UIManager == null)
        {
            Debug.LogError("UIManager not found in the scene!");
        }

        if (BrewingMachine == null)
        {
            Debug.LogError("BrewingMachine not found in the scene!");
        }
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

    public void ExitGame()
    {
        Debug.Log("Exiting the game...");
        Application.Quit();
    }
}
