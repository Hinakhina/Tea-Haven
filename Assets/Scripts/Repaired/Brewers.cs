using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Brewers : MonoBehaviour
{
    [SerializeField] Slider brewingProgressBar;
    [SerializeField] Image brewerButton; // Reference to the Button's Image component
    [SerializeField] Sprite[] brewerEmpty; // Empty brewer sprites for different tea types
    [SerializeField] Sprite[] brewerTea;   // Brewing tea sprites for different tea types
    [SerializeField] Sprite[] brewerDone;  // Brewed tea sprites for different tea types
    public float brewingTime = 3f;
    [SerializeField] ServeTea serveTeaScript; // Reference to the Button's Image component

    [SerializeField] TeaRecipe teaRecipe;

    private string currentTea = "";
    private bool hasHotWater = false;
    private bool isBrewing = false;
    private bool teaOnTable = false;

    private void Start()
    {
        ResetBrewer();
    }

    public void OnBrewerClicked()
    {
        if (isBrewing || teaOnTable) return; // Prevent interaction while brewing

        string selectedIngredient = CursorManagers.Instance.GetSelectedIngredient();

        if (selectedIngredient == "Chrysanthemum" || selectedIngredient == "Green" ||
            selectedIngredient == "Oolong" || selectedIngredient == "Lavender")
        {
            AddTea(selectedIngredient);
        }
        else if (selectedIngredient == "Hot Water")
        {
            AddHotWater();
        }
    }

    private void AddTea(string teaType)
    {
        if (!string.IsNullOrEmpty(currentTea)) return; // Prevent adding more than one tea type

        currentTea = teaType;
        brewerButton.sprite = GetBrewerSprite(brewerTea, teaType); // Change to brewing sprite
        teaRecipe.AddIngredient(teaType);
        CursorManagers.Instance.ResetSelection();
    }

    private void AddHotWater()
    {
        if (string.IsNullOrEmpty(currentTea) || hasHotWater) return;
        
        hasHotWater = true;
        CursorManagers.Instance.ResetSelection();
        // teaRecipe.AddIngredient("Hot Water");
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
        isBrewing = true;
        // yield return new WaitForSeconds(brewingTime);
        while (elapsedTime < brewingTime)
        {
            elapsedTime += Time.deltaTime;
            if (brewingProgressBar != null)
            {
                brewingProgressBar.value = elapsedTime / brewingTime;
            }
            yield return null;
        }

        brewerButton.sprite = GetBrewerSprite(brewerDone, currentTea); // Change to brewed tea sprite

        if (brewingProgressBar != null)
        {
            brewingProgressBar.gameObject.SetActive(false);
        }

        isBrewing = false;
    }

    public bool HasBrewedTea()
    {
        return !string.IsNullOrEmpty(currentTea) && !isBrewing && hasHotWater;
    }

    public string GetBrewedTeaType()
    {
        return currentTea;
    }

    public void ResetBrewer()
    {
        currentTea = "";
        hasHotWater = false;
        isBrewing = false;
        brewerButton.sprite = GetBrewerSprite(brewerEmpty, ""); // Reset to empty brewer sprite
    }

    private Sprite GetBrewerSprite(Sprite[] brewerSprites, string teaType)
    {
        switch (teaType)
        {
            case "Chrysanthemum": return brewerSprites[0];
            case "Green": return brewerSprites[1];
            case "Oolong": return brewerSprites[2];
            case "Lavender": return brewerSprites[3];
            default: return brewerSprites[0]; // Default to first sprite if no tea is selected
        }
    }

    public void TransferTeaToTable()
    {
        if (CursorManagers.Instance.IsCupOrGlassSelected() && HasBrewedTea())
        {
            bool isGlass = CursorManagers.Instance.IsGlassSelected(); // Determine cup type
            teaOnTable = true;
            brewerButton.sprite = GetBrewerSprite(brewerEmpty, ""); // Reset brewer

            serveTeaScript.PlaceTeaOnTable(currentTea, isGlass); // Pass the correct cup type

            CursorManagers.Instance.ResetSelection();
        }

    }


    public void NotifyTeaServed()
    {
        UnityEngine.Debug.Log("ResetBrewer - Tea Served");
        teaOnTable = false; // Reset only when tea is served to the customer
        ResetBrewer();
    }
}
