using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class GameplayUIManager : MonoBehaviour
{
    public static GameplayUIManager Instance { get; private set; }

    [SerializeField] private TMP_Text statusText;
    [SerializeField] private GameObject teaBrewingPanel;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private UnityEngine.UI.Slider brewingProgressBar;
    [SerializeField] private float brewingTime = 3f;
    [SerializeField] private List<string> availableIngredients;
    private List<string> selectedIngredients = new List<string>();
    private List<string> currentOrder = new List<string>();

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

    public void ShowTeaBrewingPanel(List<string> order)
    {
        if (teaBrewingPanel != null)
        {
            teaBrewingPanel.SetActive(true);
        }
        if (statusText != null)
        {
            statusText.text = "Order: " + string.Join(", ", order);
        }
        currentOrder = new List<string>(order);
        selectedIngredients.Clear();
    }

    public void SelectIngredient(string ingredient)
    {
        if (availableIngredients.Contains(ingredient))
        {
            selectedIngredients.Add(ingredient);
        }
    }

    public void ConfirmTea()
    {
        StartCoroutine(BrewTea());
    }

    private IEnumerator BrewTea()
    {
        if (brewingProgressBar != null)
        {
            brewingProgressBar.gameObject.SetActive(true);
            brewingProgressBar.value = 0f;
        }

        float elapsedTime = 0f;
        while (elapsedTime < brewingTime)
        {
            elapsedTime += Time.deltaTime;
            if (brewingProgressBar != null)
            {
                brewingProgressBar.value = elapsedTime / brewingTime;
            }
            yield return null;
        }

        if (brewingProgressBar != null)
        {
            brewingProgressBar.gameObject.SetActive(false);
        }

        if (IsCorrectOrder())
        {
            ShowSuccessMessage("Tea prepared successfully!");
        }
        else
        {
            ShowErrorMessage("Incorrect ingredients!");
        }
    }
    private bool IsCorrectOrder()
    {
        if (selectedIngredients.Count != currentOrder.Count)
            return false;

        List<string> sortedSelected = new List<string>(selectedIngredients);
        List<string> sortedOrder = new List<string>(currentOrder);
        sortedSelected.Sort();
        sortedOrder.Sort();

        for (int i = 0; i < sortedSelected.Count; i++)
        {
            if (sortedSelected[i] != sortedOrder[i])
                return false;
        }
        return true;
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
