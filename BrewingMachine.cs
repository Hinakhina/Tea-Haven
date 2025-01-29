using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BrewingMachine : MonoBehaviour
{
    public Image brewingProgressBar;
    private float brewingTime;
    private bool isBrewing = false;

    public void StartBrewing(string teaType, float time)
    {
        if (isBrewing) return;
        brewingTime = time;
        StartCoroutine(BrewTea(teaType));
    }

    private IEnumerator BrewTea(string teaType)
    {
        isBrewing = true;
        float elapsedTime = 0f;

        while (elapsedTime < brewingTime)
        {
            elapsedTime += Time.deltaTime;
            brewingProgressBar.fillAmount = elapsedTime / brewingTime;
            yield return null;
        }

        isBrewing = false;
        Debug.Log($"{teaType} is ready!");
    }
}