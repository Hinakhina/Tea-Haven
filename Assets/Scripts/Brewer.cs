using System.Collections;
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
    public void AddIngredients(TeaVariant tea)
    {
        if (hasTeaLeaves)
        {
            currentTeaVariant = tea;
            hasTeaLeaves = true;
            brewerRenderer.sprite = currentTeaVariant.teaLeavesSprite;
            Debug.Log("Leaves added.");
            StartCoroutine(BrewTea());
        }
        else if (hasTeaLeaves && !hasWater)
        {
            hasWater = true;
            brewerRenderer.sprite = currentTeaVariant.brewedSprite;
            Debug.Log("Water added. Starting brew...");
            StartCoroutine(BrewTea());
        }
        else
        {
            Debug.Log("You must add tea leaves first!");
        }
    }

    private IEnumerator BrewTea()
    {
        isBrewing = true;
        yield return new WaitForSeconds(brewTime);
        isBrewed = true;
        isBrewing = false;
        brewerRenderer.sprite = currentTeaVariant.brewedSprite;
        Debug.Log("Tea brewing complete! Ready to pour.");
    }

    public void PourTea()
    {
        if (isBrewed)
        {
            Debug.Log($"Pouring {currentTeaVariant.teaName} tea.");
            if (brewedTeaSprite != null)
            {
                SpriteRenderer pouredTeaRenderer = brewedTeaSprite.GetComponent<SpriteRenderer>();
                pouredTeaRenderer.sprite = currentTeaVariant.brewedSprite;
            }
            ResetBrewer();
        }
        else
        {
            Debug.Log("Tea is not ready yet!");
        }
    }
    private void ResetBrewer()
    {
        hasTeaLeaves = false;
        hasWater = false;
        isBrewed = false;
        brewerRenderer.sprite = null;
    }
}