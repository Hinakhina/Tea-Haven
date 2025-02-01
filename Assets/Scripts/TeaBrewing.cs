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
        selectedIngredients.Add(ingredient);
        UpdateUI();
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

    public void SubmitTea()
    {
        string brewedTea = string.Join(", ", selectedIngredients);
        Debug.Log("Brewed Tea: " + brewedTea);

        if (CheckOrder())
        {
            Debug.Log("Correct order!");
            GameplayUIManager.Instance.ShowSuccessMessage("Order correct!");
        }
        else
        {
            Debug.Log("Incorrect order!");
            GameplayUIManager.Instance.ShowErrorMessage("Wrong ingredients!");
        }

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
    private bool CheckOrder()
    {
        return selectedIngredients.Contains(currentOrder);
    }

    private void UpdateUI()
    {
        selectedIngredientsText.text = "Selected: " + string.Join(", ", selectedIngredients);
    }
}
