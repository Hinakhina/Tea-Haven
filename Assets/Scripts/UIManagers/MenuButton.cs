using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void StartNewGame()
    {
        AudioManagers.Instance.PlaySFX("dink");
        SavingManager.Instance.ResetGameData();
        GameManager.Instance.StartNewGame();
    }

    public void ContinueGame()
    {
        AudioManagers.Instance.PlaySFX("dink");
        SavingManager.Instance.LoadGameData();
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
        GameManager.Instance.UIManager.ShowCredits(); 
    }

    public void CloseCredits()
    {
        AudioManagers.Instance.PlaySFX("dink");
        GameManager.Instance.UIManager.CloseCredits(); 
    }

    public void ShowExitConfirmation()
    {
        AudioManagers.Instance.PlaySFX("dink");
        GameManager.Instance.UIManager.ShowExitConfirmation(); 
    }

    public void CancelExit()
    {
        AudioManagers.Instance.PlaySFX("dink");
        GameManager.Instance.UIManager.CancelExit(); 
    }

    public void ExitGame()
    {
        AudioManagers.Instance.PlaySFX("dink");
        GameManager.Instance.ExitGame();
    }
}
