using UnityEngine;
using UnityEngine.UI;

public class BrewingMachine : MonoBehaviour
{
    public Image brewingProgressBar;
    private bool isBrewing = false;

    public void StartBrewing()
    {
        if (!isBrewing)
        {
            isBrewing = true;
            StartCoroutine(BrewTea());
        }
    }

    private IEnumerator BrewTea()
    {
        float time = 0;
        while (time < 3)
        {
            time += Time.deltaTime;
            brewingProgressBar.fillAmount = time / 3f;
            yield return null;
        }
        isBrewing = false;
    }
}