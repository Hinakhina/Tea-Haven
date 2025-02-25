using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public UIManager UIManager { get; private set; }

    public SavingManager SavingManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        // else
        // {
        //     Destroy(gameObject);
        // }
        FindSceneReferences();
    }

    public void FindSceneReferences()
    {
        // Find UIManager and BrewingMachine in the scene
        UIManager = FindObjectOfType<UIManager>();
        SavingManager = FindObjectOfType<SavingManager>();

        if (UIManager == null)
        {
            Debug.LogError("UIManager not found in the scene!");
        }
    }


    public void StartNewGame()
    {
        AudioManagers.Instance.PlaySFX("dink");
        SavingManager.ResetGameData();
        Debug.Log("Starting a new game...");
        SceneManager.LoadScene("GamePlay");
    }

    public void ContinueGame()
    {
        AudioManagers.Instance.PlaySFX("dink");
        Debug.Log("Continuing the game...");
        SceneManager.LoadScene("GamePlay");
    }

    public void OpenSettings()
    {
        AudioManagers.Instance.PlaySFX("dink");
        Debug.Log("Opening settings...");
        SceneManager.LoadScene("SettingsPage");
    }

    public void ExitGame()
    {
        AudioManagers.Instance.PlaySFX("dink");
        Debug.Log("Exiting the game...");
        Application.Quit();
    }
}
