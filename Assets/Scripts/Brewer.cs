using System.Collections;
using UnityEngine;

public class Brewer : MonoBehaviour
{
    public float brewingTime = 3f;
    private bool isBrewing = false;

    private void Start()
    {
        ResetBrewer();
    }

    public bool AddTeaLeaves()
    {
        return OrderManager.Instance.AddTeaLeaves();
    }

    public bool AddWater()
    {
        if (OrderManager.Instance.AddWater())
        {
            StartCoroutine(BrewTea());
            return true;
        }
        return false;
    }

    private IEnumerator BrewTea()
    {
        if (isBrewing) yield break;

        isBrewing = true;
        Debug.Log("Brewing tea...");
        yield return new WaitForSeconds(brewingTime);
        isBrewing = false;
        OrderManager.Instance.BrewingComplete();
        Debug.Log("Brewing complete! Tea is ready.");
    }

    public void ResetBrewer()
    {
        isBrewing = false;
    }
}
