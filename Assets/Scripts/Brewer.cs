using System.Collections;
using UnityEngine;

public class Brewer : MonoBehaviour
{
    public bool hasTeaLeaves = false;
    public bool hasWater = false;
    public bool isBrewing = false;
    public bool isBrewed = false;
    public float brewTime = 5f;

    public GameObject brewedTea;

    public void AddIngredient(Ingredients ingredients)
    {
        if (ingredients == null)
        {
            Debug.LogWarning("Tried to add a null ingredient!");
            return;
        }

        switch (ingredients.ingredientsType)
        {
            case Ingredients.IngredientsType.TeaLeaves:
                if (!hasTeaLeaves)
                {
                    hasTeaLeaves = true;
                    Debug.Log("Tea leaves added.");
                }
                else
                {
                    Debug.Log("Tea leaves are already added.");
                }
                break;

            case Ingredients.IngredientsType.Water:
                if (hasTeaLeaves && !hasWater)
                {
                    hasWater = true;
                    Debug.Log("Water added. Starting brew...");
                    StartCoroutine(BrewTea());
                }
                else if (!hasTeaLeaves)
                {
                    Debug.Log("Add tea leaves first before adding water.");
                }
                else
                {
                    Debug.Log("Water is already added.");
                }
                break;

            default:
                Debug.Log("This ingredient is not needed in the brewer.");
                break;
        }
    }

    private IEnumerator BrewTea()
    {
        isBrewing = true;
        yield return new WaitForSeconds(brewTime);
        isBrewed = true;
        isBrewing = false;
        Debug.Log("Tea brewing complete! Ready to pour.");
    }

    public GameObject PourTea()
    {
        if (isBrewed)
        {
            Debug.Log("Pouring tea into cup/glass.");
            return brewedTea;
        }
        Debug.Log("Tea is not ready yet!");
        return null;
    }
}
