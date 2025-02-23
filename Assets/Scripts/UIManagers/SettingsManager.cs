using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public void GoBackToMainMenu()
    {
        AudioManagers.Instance.PlaySFX("dink");
        SceneManager.LoadScene("MainMenu");
    }
}
