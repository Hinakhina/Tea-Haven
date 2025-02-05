using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void StartNewGame()
    {
        AudioManagers.Instance.PlaySFX("dink");
        GameManager.Instance.StartNewGame();
    }

    public void ContinueGame()
    {
        AudioManagers.Instance.PlaySFX("dink");
        GameManager.Instance.ContinueGame();
    }

    public void OpenSettings()
    {
        AudioManagers.Instance.PlaySFX("dink");
        GameManager.Instance.OpenSettings();
    }

    public void ShowCredits()
    {
        AudioManagers.Instance.PlaySFX("dink");
        GameManager.Instance.UIManager.ShowCredits();  // ✅ FIXED
    }

    public void CloseCredits()
    {
        AudioManagers.Instance.PlaySFX("dink");
        GameManager.Instance.UIManager.CloseCredits(); // ✅ FIXED
    }

    public void ShowExitConfirmation()
    {
        AudioManagers.Instance.PlaySFX("dink");
        GameManager.Instance.UIManager.ShowExitConfirmation(); // ✅ FIXED
    }

    public void CancelExit()
    {
        AudioManagers.Instance.PlaySFX("dink");
        GameManager.Instance.UIManager.CancelExit(); // ✅ FIXED
    }

    public void ExitGame()
    {
        AudioManagers.Instance.PlaySFX("dink");
        GameManager.Instance.ExitGame();
    }
}
