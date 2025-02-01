//using System.Collections;
//using UnityEngine;

//public class BrewingMachine : MonoBehaviour
//{
//    private string selectedTea;
//    private bool isBrewing = false;

//    public void SetTeaType(string teaType)
//    {
//        selectedTea = teaType;
//        GameManager.Instance.UIManager.UpdateStatusText("Selected: " + teaType);
//    }

//    public bool HasTea()
//    {
//        return !string.IsNullOrEmpty(selectedTea);
//    }

//    public void StartBrewing()
//    {
//        if (!isBrewing && HasTea())
//        {
//            isBrewing = true;
//            GameManager.Instance.UIManager.UpdateStatusText("Brewing...");
//            StartCoroutine(BrewingProcess());
//        }
//        else
//        {
//            GameManager.Instance.UIManager.UpdateStatusText("Select tea leaves first!");
//        }
//    }

//    private IEnumerator BrewingProcess()
//    {
//        yield return new WaitForSeconds(3);
//        GameManager.Instance.UIManager.UpdateStatusText("Brewing complete!");
//        isBrewing = false;
//    }

//    public string GetBrewedTea()
//    {
//        return selectedTea;
//    }

//    public void ClearTea()
//    {
//        selectedTea = null;
//    }
//}