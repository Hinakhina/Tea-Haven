using UnityEngine;
public class SavingManager : MonoBehaviour
{
    public static SavingManager Instance;
    public int Coins;
    public int DayCount;
   
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGameData();
        }
        else
        {
            Destroy(gameObject);
        }
    }
   
    public void AddCoins(int amount)
    {
        Coins += amount;
    }

    public void AddDays()
    {
        DayCount += 1;
    }
   
    public void CompleteDay()
    {
        
        SaveGameData();
    }
   
    public void SaveGameData()
    {
        Debug.Log($"Coins : {Coins}");
        Debug.Log($"DayCount : {DayCount}");
        PlayerPrefs.SetInt("Coins", Coins);
        PlayerPrefs.SetInt("DayCount", DayCount);
        PlayerPrefs.Save();
    }
   
    public void LoadGameData()
    {
        Coins = 0;
        DayCount = 0;
        Coins = PlayerPrefs.GetInt("Coins", 0);
        DayCount = PlayerPrefs.GetInt("DayCount", 1);
    }
   
    public void ResetGameData()
    {
        Debug.Log("Reset Data");
        Coins = 0;
        DayCount = 1;
        SaveGameData();
    }
}