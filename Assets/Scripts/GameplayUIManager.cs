using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameplayUIManager : MonoBehaviour
{
    public static GameplayUIManager Instance { get; private set; }

    [SerializeField] private TMP_Text statusText;
    [SerializeField] private GameObject teaBrewingPanel;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private List<string> availableIngredients;
    private List<string> selectedIngredients = new List<string>();
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

    public void UpdateStatusText(string message)
    {
        if (statusText != null)
        {
            statusText.text = message;
        }
    }

    public void ShowTeaBrewingPanel(string tea)
    {
        if (teaBrewingPanel != null)
        {
            teaBrewingPanel.SetActive(true);
            statusText.text = tea;
        }
    }

    private bool IsCorrectOrder()
    {
        return selectedIngredients.Count > 0 && string.Join(", ", selectedIngredients) == currentOrder;
    }

    public void HideTeaBrewingPanel()
    {
        if (teaBrewingPanel != null)
        {
            teaBrewingPanel.SetActive(false);
        }
    }

    public void ShowSuccessMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
            messageText.color = Color.green;
        }
    }

    public void ShowErrorMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
            messageText.color = Color.red;
        }
    }
}
