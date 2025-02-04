using System.Collections;
using UnityEngine;

public class Brewer : MonoBehaviour
{
    public TeaVariant currentTeaVariant;
    public SpriteRenderer brewerRenderer;

    private bool hasTeaLeaves = false;
    private bool hasWater = false;
    private bool isBrewing = false;
    public bool isBrewed = false;
    public float brewTime = 5f;

    public GameObject brewedTeaPrefab;

    public void AddTeaLeaves(TeaVariant tea)
    {
        if (!hasTeaLeaves)
        {
            currentTeaVariant = tea;
            hasTeaLeaves = true;
            brewerRenderer.sprite = tea.teaLeavesSprite;
            Debug.Log($"{tea.teaName} leaves added.");
        }
    }

    public void AddWater()
    {
        if (hasTeaLeaves && !hasWater)
        {
            hasWater = true;
            brewerRenderer.sprite = currentTeaVariant.brewingSprite;
            Debug.Log("Water added. Brewing started...");
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

    public GameObject PourTea()
    {
        if (isBrewed)
        {
            Debug.Log($"Pouring {currentTeaVariant.teaName} tea.");
            return brewedTeaPrefab;
        }
        Debug.Log("Tea is not ready yet!");
        return null;
    }
}
