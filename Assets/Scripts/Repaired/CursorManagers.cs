using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class CursorManagers : MonoBehaviour
{
    public static CursorManagers Instance;
    
    // [SerializeField] Texture2D defaultCursor;
    [SerializeField] Texture2D chrysanthemumCursor;
    [SerializeField] Texture2D greenCursor;
    [SerializeField] Texture2D oolongCursor;
    [SerializeField] Texture2D lavenderCursor;
    [SerializeField] Texture2D iceCursor;
    [SerializeField] Texture2D sugarCursor;
    [SerializeField] Texture2D milkCursor;
    [SerializeField] Texture2D hotWaterCursor;
    [SerializeField] Texture2D cupCursor;
    [SerializeField] Texture2D glassCursor;

    private string selectedIngredient = "";

    private List<string> availableIngredients = new List<string>{ "Chrysanthemum", "Sugar", "Hot Water", "Green", "Oolong", "Lavender", "Milk", "Ice", "Cup", "Glass"};

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SelectIngredient(string ingredient)
    {
        if (availableIngredients.Contains(ingredient))
        {
            selectedIngredient = ingredient;
            UpdateCursor(ingredient);
        }
    }

    private void UpdateCursor(string ingredient)
    {
        Texture2D cursorImage = null;

        switch (ingredient)
        {
            case "Chrysanthemum":
                AudioManagers.Instance.PlaySFX("leaves");
                cursorImage = chrysanthemumCursor;
                break;
            case "Green":
                AudioManagers.Instance.PlaySFX("leaves");
                cursorImage = greenCursor;
                break;
            case "Oolong":
                AudioManagers.Instance.PlaySFX("leaves");
                cursorImage = oolongCursor;
                break;
            case "Lavender":
                AudioManagers.Instance.PlaySFX("leaves");
                cursorImage = lavenderCursor;
                break;
            case "Ice":
                AudioManagers.Instance.PlaySFX("ice");
                cursorImage = iceCursor;
                break;
            case "Sugar":
                AudioManagers.Instance.PlaySFX("sugar");
                cursorImage = sugarCursor;
                break;
            case "Milk":
                AudioManagers.Instance.PlaySFX("milk");
                cursorImage = milkCursor;
                break;
            case "Hot Water":
                AudioManagers.Instance.PlaySFX("water");
                StartCoroutine(waiting());
                cursorImage = hotWaterCursor;
                break;
            case "Cup":
                AudioManagers.Instance.PlaySFX("cup");
                cursorImage = cupCursor;
                break;
            case "Glass":
                AudioManagers.Instance.PlaySFX("glass");
                cursorImage = glassCursor;
                break;
        }

        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
    }

    public IEnumerator waiting(){
        yield return new WaitForSeconds(3.0f);
    }

    public string GetSelectedIngredient()
    {
        return selectedIngredient;
    }

    public void ResetSelection()
    {
        selectedIngredient = "";
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public bool IsGlassSelected()
    {
        return selectedIngredient == "Glass";
    }

    public bool IsCupOrGlassSelected()
    {
        return selectedIngredient == "Cup" || selectedIngredient == "Glass";
    }
}
