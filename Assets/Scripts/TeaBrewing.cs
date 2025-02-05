using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeaBrewing : MonoBehaviour
{
    [SerializeField] private List<string> availableIngredients;
    public TextMeshProUGUI selectedIngredientsText;
    private List<string> selectedIngredients = new List<string>();
    private string currentOrder;

    public void SetOrder(string order)
    {
        currentOrder = order;
        selectedIngredients.Clear();
        UpdateUI();
    }

    public void AddIngredient(string ingredient)
    {
        if (availableIngredients.Contains(ingredient) && !selectedIngredients.Contains(ingredient))
        {
            selectedIngredients.Add(ingredient);
            UpdateUI();
        }
    }

    public void SelectIngredient(string ingredient)
    {
        if (availableIngredients.Contains(ingredient))
        {
            selectedIngredients.Add(ingredient);
        }
    }
    public void StartBrewing(string order)
    {
        currentOrder = order;
        selectedIngredients.Clear();
    }

    private bool ValidateOrder()
    {
        return string.Join(", ", selectedIngredients) == currentOrder;
    }
    public void SubmitTea()
    {
        if (ValidateOrder())
        {
            Debug.Log("Correct order!");
            GameplayUIManager.Instance.ShowSuccessMessage("Order correct!");
            ResetBrewing();
        }
        else
        {
            Debug.Log("Incorrect order!");
            GameplayUIManager.Instance.ShowErrorMessage("Wrong ingredients!");
        }
    }

    private void ResetBrewing()
    {
        selectedIngredients.Clear();
        UpdateUI();
    }
    private bool IsCorrectOrder()
    {
        return selectedIngredients.Count > 0 && string.Join(", ", selectedIngredients) == currentOrder;
    }
    public void ConfirmTea()
    {
        if (IsCorrectOrder())
        {
            GameplayUIManager.Instance.ShowSuccessMessage("Tea prepared successfully!");
        }
        else
        {
            GameplayUIManager.Instance.ShowErrorMessage("Incorrect ingredients!");
        }
    }

    private void UpdateUI()
    {
        selectedIngredientsText.text = "Selected: " + string.Join(", ", selectedIngredients);
    }
}
