using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ServeTea : MonoBehaviour
{
    public Image teaOnTableButton; // Tea button on the table
    public Sprite[] teaSprites; // 8 variations: 4 for glass, 4 for cup
    public Sprite[] milkTeaSprites; // 8 variations: 4 for glass, 4 for cup
    private string currentTea = "";
    private bool isGlass = false; // Determines if ice can be added
    [SerializeField] Brewers brewerScript;

    [SerializeField] TeaRecipe teaRecipe;
    [SerializeField] OrderManagers orderManagers;


    private void Start()
    {
        teaOnTableButton.gameObject.SetActive(false);
    }

    public void PlaceTeaOnTable(string teaType, bool usedGlass)
    {
        currentTea = teaType;
        isGlass = usedGlass;
        if(isGlass){
            teaRecipe.AddIngredient("Glass");
        }
        else{
            teaRecipe.AddIngredient("Cup");
        }

        teaOnTableButton.sprite = GetTeaSprite(teaType, isGlass); // Default without ice
        teaOnTableButton.gameObject.SetActive(true);
    }

    public void OnTeaClicked()
    {
        if (string.IsNullOrEmpty(currentTea)) return;

        string selectedIngredient = CursorManagers.Instance.GetSelectedIngredient();

        if (selectedIngredient == "Ice" && isGlass)
        {
            UnityEngine.Debug.Log("Is Ice");
            AddIce();
        }
        else if (selectedIngredient == "Ice" && !isGlass)
        {
            UnityEngine.Debug.Log("Cannot add ice to a cup!");
        }
        else if (selectedIngredient == "Sugar")
        {
            AudioManagers.Instance.PlaySFX("sugar");
            teaRecipe.AddIngredient(selectedIngredient); // Store sugar/milk
            CursorManagers.Instance.ResetSelection();
        }
        else if (selectedIngredient == "Milk")
        {
            AudioManagers.Instance.PlaySFX("milk");
            teaRecipe.AddIngredient(selectedIngredient); // Store sugar/milk
            teaOnTableButton.sprite = GetMilkTeaSprite(currentTea, isGlass);
            CursorManagers.Instance.ResetSelection();
        }
        else if (selectedIngredient == "")
        {
            ServeToCustomer();
        }
    }

    private void AddIce()
    {
        AudioManagers.Instance.PlaySFX("ice");
        teaRecipe.AddIngredient("Ice");
        CursorManagers.Instance.ResetSelection();
    }

    private void ServeToCustomer()
    {
        AudioManagers.Instance.PlaySFX("sip");
        teaOnTableButton.gameObject.SetActive(false);
        StartCoroutine(waitingCheckOrder());
        currentTea = "";
        brewerScript.NotifyTeaServed();
        
    }

    private Sprite GetTeaSprite(string teaType, bool useGlass)
    {
        int index = useGlass ? 4 : 0; // Glass sprites start from index 4
        switch (teaType)
        {
            case "Chrysanthemum": return teaSprites[index];
            case "Green": return teaSprites[index + 1];
            case "Oolong": return teaSprites[index + 2];
            case "Lavender": return teaSprites[index + 3];
            default: return null;
        }
    }

    private Sprite GetMilkTeaSprite(string teaType, bool useGlass)
    {
        int index = useGlass ? 4 : 0; // Glass sprites start from index 4
        switch (teaType)
        {
            case "Chrysanthemum": return milkTeaSprites[index];
            case "Green": return milkTeaSprites[index + 1];
            case "Oolong": return milkTeaSprites[index + 2];
            case "Lavender": return milkTeaSprites[index + 3];
            default: return null;
        }
    }
    

    public void ResetTea(){
        teaOnTableButton.gameObject.SetActive(false);
        currentTea = "";
        brewerScript.NotifyTeaServed();
    }

    public IEnumerator waitingCheckOrder(){
        yield return new WaitForSeconds(1.0f);
        orderManagers.CheckOrder();
    }
}
