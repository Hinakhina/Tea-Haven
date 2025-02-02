using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class GameplayUIManager : MonoBehaviour
{
    public static GameplayUIManager Instance { get; private set; }
    public PauseManager PauseManager { get; private set; }

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
            availableIngredients = new List<string> { "Chrysantemum Tea Leaves", "Sugar", "Hot Water", "Matcha Powder", "Oolong Tea Leaves", "Lavender Tea Leaves" }; // add more ingredients here 
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        PauseManager = FindObjectOfType<PauseManager>();

        if (PauseManager == null)
        {
            Debug.LogError("PauseManager not found in the scene!");
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
        Debug.Log($"ShowTeaBrewingPanel called with tea: {teaName}");
        Debug.Log($"Received ingredients: {string.Join(", ", orderIngredients)}");
        if (teaBrewingPanel != null)
        {
            teaBrewingPanel.SetActive(true);
        }
        if (statusText != null)
        {
            statusText.text = "Order: " + teaName;
        }
        Debug.Log($"Setting current order to: {string.Join(", ", orderIngredients)}");
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
        UpdateSelectedIngredientsUI();
    }

    public void ConfirmTea()
    {
        if (selectedIngredients.Count == 0)
        {
            ShowErrorMessage("No ingredients selected!");
            return;
        }

        List<string> brewingIngredients = new List<string>(selectedIngredients);
        StartCoroutine(BrewTea(brewingIngredients));
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

        bool isCorrect = IsCorrectOrder(brewingIngredients);
        if (isCorrect)
        {
            ShowSuccessMessage("Tea prepared successfully!");
        }
        else
        {
            ShowErrorMessage("Incorrect ingredients!");
        }

        FindObjectOfType<CustomerOrder>().ReceiveTea(isCorrect);
    }
    private bool IsCorrectOrder(List<string> brewingIngredients)
    {
        Debug.Log("===== Order Comparison =====");
        Debug.Log($"Selected ingredients ({brewingIngredients.Count}): {string.Join(", ", brewingIngredients)}");
        Debug.Log($"Expected ingredients ({currentOrder.Count}): {string.Join(", ", currentOrder)}");
        if (brewingIngredients.Count != currentOrder.Count)
        {
            Debug.Log($"Count mismatch! Selected: {brewingIngredients.Count}, Expected: {currentOrder.Count}");
            return false;
        }

        for (int i = 0; i < brewingIngredients.Count; i++)
        {
            Debug.Log($"Comparing position {i}: Selected '{brewingIngredients[i]}' vs Expected '{currentOrder[i]}'");
            if (brewingIngredients[i] != currentOrder[i])
            {
                Debug.Log($"Mismatch at position {i}");
                return false;
            }
        }
        Debug.Log("All ingredients match!");
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
