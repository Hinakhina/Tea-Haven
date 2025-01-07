using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField] GameObject creditScreen;
    [SerializeField] GameObject exitingScreen;

    public void Start(){
        creditScreen.SetActive(false);
        exitingScreen.SetActive(false);
    }

    // Start is called before the first frame update
    public void NewGameButton(){
        //AudioManager.Instance.PlaySFX("Nama Sound");
        SceneManager.LoadScene("GamePlay");
        Debug.Log("Start a new game...");
    }

    public void ContinueButton(){
        //AudioManager.Instance.PlaySFX("Nama Sound");
        SceneManager.LoadScene("GamePlay");
    }

    public void SettingsButton(){
        //AudioManager.Instance.PlaySFX("Nama Sound");
        SceneManager.LoadScene("SettingsPage");
    }

    public void CreditsButton(){
        //AudioManager.Instance.PlaySFX("Nama Sound");
        creditScreen.SetActive(true);
        
    }

    public void CloseCreditsButton(){
        //AudioManager.Instance.PlaySFX("Nama Sound");
        creditScreen.SetActive(false);
    }

    public void ExitButton(){
        //AudioManager.Instance.PlaySFX("Nama Sound");
        exitingScreen.SetActive(true);
    }

    public void NoExitButton(){
        //AudioManager.Instance.PlaySFX("Nama Sound");
        exitingScreen.SetActive(false);
    }

    public void YesExitButton(){
        //AudioManager.Instance.PlaySFX("Nama Sound");
        Application.Quit(); //exit apps
    }
}
