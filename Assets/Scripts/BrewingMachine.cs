using System.Collections;
using UnityEngine;

public class BrewingMachine : MonoBehaviour
{
    private string selectedTeaType;
    private bool isBrewing;

    public void SetTeaType(string teaType)
    {
        selectedTeaType = teaType;
        Debug.Log("Tea type selected: " + teaType);
    }

    public bool HasTea()
    {
        return !string.IsNullOrEmpty(selectedTeaType);
    }

    public void StartBrewing()
    {
        if (HasTea() && !isBrewing)
        {
            isBrewing = true;
            StartCoroutine(BrewingProcess());
        }
        else
        {
            Debug.Log("No tea selected or already brewing.");
        }
    }

    private IEnumerator BrewingProcess()
    {
        Debug.Log("Brewing started...");
        yield return new WaitForSeconds(3);
        Debug.Log("Brewing complete!");
        isBrewing = false;
    }

    public string GetBrewedTea()
    {
        if (!isBrewing && HasTea())
        {
            return selectedTeaType;
        }
        return null;
    }

    public void ClearTea()
    {
        selectedTeaType = null;
        Debug.Log("Tea cleared from machine.");
    }
}