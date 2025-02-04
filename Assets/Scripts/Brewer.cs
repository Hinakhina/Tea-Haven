using System.Collections;
using UnityEngine;

public class Brewer : MonoBehaviour
{
    public float brewingTime = 3f;
    public bool hasTeaLeaves = false;
    public bool isBrewing = false;
    public bool isBrewed = false;
    public bool hasWater = false;

    public GameObject brewedTeaPrefab;

    public void AddIngredient(GameObject ingredient)
    {
        if (ingredient.CompareTag("Tea Leaves") && !hasTeaLeaves)
        {
            hasTeaLeaves = true;
            Debug.Log("Tea leaves added.");
        }
        else if (ingredient.CompareTag("Water") && hasTeaLeaves && !hasWater)
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
        Debug.Log("Brewing tea...");
        yield return new WaitForSeconds(brewingTime);
        isBrewing = false;
        OrderManager.Instance.BrewingComplete();
        Debug.Log("Brewing complete! Tea is ready.");
    }

    public GameObject PourTea()
    {
        if (isBrewed)
        {
            Debug.Log("Pouring tea into cup/glass.");
            return Instantiate(brewedTeaPrefab); // Creates the brewed tea object
        }
        Debug.Log("Tea is not ready yet!");
        return null;
    }
}
