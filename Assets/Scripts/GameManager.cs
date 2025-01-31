using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public UIManager UIManager { get; private set; }

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
        FindSceneReferences();
    }

    public void FindSceneReferences()
    {
        // Find UIManager and BrewingMachine in the scene
        UIManager = FindObjectOfType<UIManager>();

        if (UIManager == null)
        {
            Debug.LogError("UIManager not found in the scene!");
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
