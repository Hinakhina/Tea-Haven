using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderManagers : MonoBehaviour
{
    [SerializeField] GameObject bubble;
    [SerializeField] public GameObject textbubble;
    [SerializeField] Image teaLeafImage, sugarMilkImage, iceImage, completedTeaImage;
    [SerializeField] Sprite[] teaSprites; // Tea leaf sprites
    [SerializeField] Sprite[] completedTeaSprites; // Completed tea sprites
    [SerializeField] Sprite sugarSprite, milkSprite, emptySugarMilkSprite;
    [SerializeField] Sprite iceSprite, emptyIceSprite;
    [SerializeField] TeaRecipe teaRecipe;
    [SerializeField] CustomersSpawner CustomersSpawner;

    [SerializeField] public TextMeshProUGUI feedbackText; 

    private List<string> currentOrder = new List<string>(); // Stores generated order
    private bool orderActive = false;
    private bool isGlass;

    public void Start(){
        bubble.SetActive(false);
        textbubble.SetActive(false);
    }

    public void GenerateNewOrder()
    {
        if (orderActive) return; // Only 1 order at a time

        bubble.SetActive(true);
        currentOrder.Clear();
        string teaLeaf = GetRandomTeaLeaf();
        bool hasIce = Random.value > 0.5f;
        isGlass = hasIce;

        currentOrder.Add(teaLeaf);
        currentOrder.Add(isGlass ? "Glass" : "Cup");

        string sugarMilk = GetRandomSugarMilk(); // Randomly decide sugar/milk/none
        if (!string.IsNullOrEmpty(sugarMilk)) currentOrder.Add(sugarMilk);

        if (hasIce) currentOrder.Add("Ice");

        UpdateOrderUI(teaLeaf, isGlass, sugarMilk, hasIce);

        Debug.Log("New Order Arrived: " + string.Join(", ", currentOrder));

        orderActive = true;

    }

    private string GetRandomTeaLeaf()
    {
        string[] teaOptions = { "Chrysanthemum", "Green", "Oolong", "Lavender" };
        return teaOptions[Random.Range(0, teaOptions.Length)];
    }

    private string GetRandomSugarMilk()
    {
        int choice = Random.Range(0, 3); // 0 = None, 1 = Sugar, 2 = Milk
        return choice == 1 ? "Sugar" : choice == 2 ? "Milk" : null;
    }

    private void UpdateOrderUI(string tea, bool isGlass, string sugarMilk, bool hasIce)
    {
        teaLeafImage.sprite = GetTeaSprite(tea);
        sugarMilkImage.sprite = sugarMilk == "Sugar" ? sugarSprite :
                                sugarMilk == "Milk" ? milkSprite : emptySugarMilkSprite;
        iceImage.sprite = hasIce ? iceSprite : emptyIceSprite;
        completedTeaImage.sprite = GetCompletedTeaSprite(tea, isGlass);

        SetImageAlpha(sugarMilkImage, !string.IsNullOrEmpty(sugarMilk));
        SetImageAlpha(iceImage, hasIce);
    }

    private void SetImageAlpha(Image img, bool isVisible)
    {
        Color color = img.color;
        color.a = isVisible ? 1f : 0f; // 1 = fully visible, 0 = invisible
        img.color = color;
    }

    private Sprite GetTeaSprite(string tea)
    {
        switch (tea)
        {
            case "Chrysanthemum": return teaSprites[0];
            case "Green": return teaSprites[1];
            case "Oolong": return teaSprites[2];
            case "Lavender": return teaSprites[3];
            default: return null;
        }
    }

    private Sprite GetCompletedTeaSprite(string tea, bool isGlass)
    {
        int index = isGlass ? 4 : 0; // Glass sprites start from index 4
        switch (tea)
        {
            case "Chrysanthemum": return completedTeaSprites[index];
            case "Green": return completedTeaSprites[index + 1];
            case "Oolong": return completedTeaSprites[index + 2];
            case "Lavender": return completedTeaSprites[index + 3];
            default: return null;
        }
    }

    public void CheckOrder()
    {
        List<string> brewedTea = teaRecipe.GetRecipe();

        Debug.Log("Order Tea: " + string.Join(", ", currentOrder));
        Debug.Log("Brewed Tea: " + string.Join(", ", brewedTea));

        bool orderResult = IsOrderCorrect(brewedTea);
        bubble.SetActive(false);

        if (orderResult)
        {
            UnityEngine.Debug.Log("Correct Order!");
            textbubble.SetActive(true);
            feedbackText.text = "Thank you!";
        }
        else
        {
            UnityEngine.Debug.Log("Wrong Order!");
            textbubble.SetActive(true);
            feedbackText.text = "I didn't order this!";
        }

        orderActive = false;
        teaRecipe.ResetRecipe();
        UnityEngine.Debug.Log("Customer is Leaving...");
        CustomersSpawner.ServeTeaFeedBack();
    }

    private bool IsOrderCorrect(List<string> brewedTea)
    {
        if (brewedTea.Count != currentOrder.Count) return false;
        
        // Sort both lists alphabetically before comparison
        brewedTea.Sort();
        currentOrder.Sort();

        
        for (int i = 0; i < brewedTea.Count; i++)
        {
            if (brewedTea[i] != currentOrder[i]) return false;
        }
        return true;
    }
}
