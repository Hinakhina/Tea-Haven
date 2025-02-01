using UnityEngine;
using System.Collections;

public class BrewingMachine : MonoBehaviour
{
    public static BrewingMachine Instance { get; private set; }

    [SerializeField] private float brewingTime = 3f;
    private bool isBrewing = false;
    private GameplayUIManager uiManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        uiManager = GameplayUIManager.Instance;
    }

    public void StartBrewing()
    {
        if (!isBrewing)
        {
            StartCoroutine(BrewTea());
        }
    }

    private IEnumerator BrewTea()
    {
        isBrewing = true;
        uiManager.ShowSuccessMessage("Brewing in progress...");
        yield return new WaitForSeconds(brewingTime);
        uiManager.ShowSuccessMessage("Tea is ready!");
        isBrewing = false;
    }
}