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
    [SerializeField] private TMP_Text selectedIngredientsText;

    private List<string> selectedIngredients = new List<string>();
    private List<string> currentOrder = new List<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            availableIngredients = new List<string> { "Tea", "Sugar", "Milk", "Matcha Powder" }; // add more ingredients here 
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

    public void ShowTeaBrewingPanel(string teaName, List<string> orderIngredients)
    {
        if (teaBrewingPanel != null)
        {
            teaBrewingPanel.SetActive(true);
        }
        if (statusText != null)
        {
            statusText.text = "Order: " + string.Join(", ", teaName);
        }
        currentOrder = new List<string>(orderIngredients);
        selectedIngredients.Clear();
    }

    public void SelectIngredient(string ingredient)
    {
        if (availableIngredients.Contains(ingredient))
        {
            selectedIngredients.Add(ingredient);
            UpdateSelectedIngredientsUI();
        }
    }

    private void UpdateSelectedIngredientsUI()
    {
        if (selectedIngredientsText != null)
        {
            selectedIngredientsText.text = "Selected Ingredients: " + string.Join(", ", selectedIngredients);
        }
    }

    public void ClearSelectedIngredients()
    {
        selectedIngredients.Clear();
        UpdateSelectedIngredientsUI(); // Refresh the UI
    }

    public void ConfirmTea()
    {
        if (selectedIngredients.Count == 0)
        {
            ShowErrorMessage("No ingredients selected!");
            return;
        }

        List<string> brewingIngredients = new List<string>(selectedIngredients);
        Debug.Log("Comparing orders:");
        Debug.Log($"Selected count: {brewingIngredients.Count}, Current order count: {currentOrder.Count}");

        StartCoroutine(BrewTea(brewingIngredients));
        Debug.Log("Selected Ingredients: " + string.Join(", ", selectedIngredients));
        Debug.Log("Expected Ingredients: " + string.Join(", ", currentOrder));
        selectedIngredients.Clear();
        UpdateSelectedIngredientsUI();
    }

    private IEnumerator BrewTea(List<string> brewingIngredients)
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

        if (IsCorrectOrder(brewingIngredients))
        {
            ShowSuccessMessage("Tea prepared successfully!");
        }
        else
        {
            ShowErrorMessage("Incorrect ingredients!");
        }
    }
    private bool IsCorrectOrder(List<string> brewingIngredients)
    {
        Debug.Log("Comparing orders:");
        Debug.Log($"Selected count: {brewingIngredients.Count}, Current order count: {currentOrder.Count}");
        if (brewingIngredients.Count != currentOrder.Count)
            return false;

        List<string> sortedSelected = new List<string>(brewingIngredients);
        List<string> sortedOrder = new List<string>(currentOrder);
        sortedSelected.Sort();
        sortedOrder.Sort();

        Debug.Log("Sorted Selected: " + string.Join(", ", sortedSelected));
        Debug.Log("Sorted Order: " + string.Join(", ", sortedOrder));

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
