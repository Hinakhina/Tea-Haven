using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void StartNewGame()
    {
        GameManager.Instance.StartNewGame();
    }

    public void ContinueGame()
    {
        GameManager.Instance.ContinueGame();
    }

    public void OpenSettings()
    {
        GameManager.Instance.OpenSettings();
    }

    public void ShowCredits()
    {
        GameManager.Instance.UIManager.ShowCredits();  // ✅ FIXED
    }

    public void CloseCredits()
    {
        GameManager.Instance.UIManager.CloseCredits(); // ✅ FIXED
    }

    public void ShowExitConfirmation()
    {
        GameManager.Instance.UIManager.ShowExitConfirmation(); // ✅ FIXED
    }

    public void CancelExit()
    {
        GameManager.Instance.UIManager.CancelExit(); // ✅ FIXED
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }
}
