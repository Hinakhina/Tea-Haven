using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Brewer : MonoBehaviour
{

    private bool hasTeaLeaves = false;
    private bool hasWater = false;
    public bool isBrewing = false;
    public bool isBrewed = false;
    public float brewTime = 5f;

    public TeaVariant currentTeaVariant;
    public SpriteRenderer brewerRenderer;
    public GameObject brewedTeaSprite;
    public Sprite emptyBrewerSprite;
    public List<TeaVariant> availableTeaVariants;
    public void AddIngredients(TeaVariant tea)
    {
        Debug.Log($"Trying to add: {tea}");
        if (!hasTeaLeaves)
        {
            currentTeaVariant = tea;
            hasTeaLeaves = true;
            brewerRenderer.sprite = currentTeaVariant.teaLeavesSprite;
            Debug.Log("Leaves added.");
        }
        else if (hasTeaLeaves && !hasWater)
        {
            hasWater = true;
            Debug.Log("Water added. Starting brew...");
            StartCoroutine(BrewTea());
        }
        else
        {
            Debug.Log("Brewing in Progress or already complete!");
        }
    }

    public void AddWater()
    {
        if (hasTeaLeaves && !hasWater)
        {
            hasWater = true;
            Debug.Log("Water added. Starting brew...");
            StartCoroutine(BrewTea());
        }
        else if (!hasTeaLeaves)
        {
            Debug.Log("Add tea leaves first!");
        }
        else if (hasWater)
        {
            Debug.Log("Water already added!");
        }
    }

    public void AddIngredientsFromInspector(int teaVariantIndex)
    {
        if (teaVariantIndex >= 0 && teaVariantIndex < availableTeaVariants.Count)
        {
            AddIngredients(availableTeaVariants[teaVariantIndex]);
        }
        else
        {
            Debug.LogError("Invalid tea variant index");
        }
    }

    private IEnumerator BrewTea()
    {
        isBrewing = true;
        brewerRenderer.sprite = currentTeaVariant.brewingSprite;
        yield return new WaitForSeconds(brewTime);
        isBrewed = true;
        isBrewing = false;
        brewerRenderer.sprite = currentTeaVariant.brewedSprite;
        Debug.Log("Tea brewing complete! Ready to pour.");
    }

    public TeaVariant PourTea()
    {
        if (isBrewed)
        {
            Debug.Log($"Pouring {currentTeaVariant.teaName}.");
            if (brewedTeaSprite != null)
            {
                SpriteRenderer pouredTeaRenderer = brewedTeaSprite.GetComponent<SpriteRenderer>();
                pouredTeaRenderer.sprite = currentTeaVariant.brewedSprite;
            }
            TeaVariant pouredTea = currentTeaVariant;
            ResetBrewer();
            return pouredTea;
        }
        else
        {
            Debug.Log("Tea is not ready yet!");
            return null;
        }
    }
    private void ResetBrewer()
    {
        hasTeaLeaves = false;
        hasWater = false;
        isBrewed = false;
        isBrewing = false;
        brewerRenderer.sprite = emptyBrewerSprite;
        currentTeaVariant = null;
    }
}