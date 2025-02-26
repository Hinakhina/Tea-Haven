using System.Collections.Generic;
using UnityEngine;

public class TeaRecipe : MonoBehaviour
{
    private ServeTea serveTeaScript;
    private List<string> currentRecipe = new List<string>(); // Stores ingredients used

    public void AddIngredient(string ingredient)
    {
        if (!currentRecipe.Contains(ingredient)) // Prevent duplicates (except sugar/milk/ice)
        {
            currentRecipe.Add(ingredient);
        }
        Debug.Log("Current Recipe: " + string.Join(", ", currentRecipe));
    }

    public List<string> GetRecipe()
    {
        return new List<string>(currentRecipe);
    }

    public void ResetRecipe()
    {
        currentRecipe.Clear();
        Debug.Log("Recipe reset.");
    }

    public void TrashButton(){
        AudioManagers.Instance.PlaySFX("trash");
    }
}
