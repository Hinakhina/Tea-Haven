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

    public void AddIngredient(string ingredient)
    {
        if (ingredient == "Tea Leaves" && !hasTeaLeaves)
        {
            hasTeaLeaves = true;
            Debug.Log("Tea leaves added.");
        }
        else if (ingredient == "Water" && hasTeaLeaves && !hasWater)
        {
            hasWater = true;
            Debug.Log("Water added. Starting brew...");
            StartCoroutine(BrewTea());
        }
        else
        {
            Debug.Log("Invalid sequence! Add tea leaves first, then water.");
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

