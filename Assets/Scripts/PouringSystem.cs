using UnityEngine;

public class PouringSystem : MonoBehaviour
{
    public Brewer brewer;
    public bool isPoured = false;
    public GameObject cup;
    public GameObject glass;
    public bool hasIce = false;

    public void PourIntoContainer(GameObject container)
    {
        if (!brewer.isBrewed)
        {
            Debug.Log("Tea is not ready yet!");
            return;
        }

        if (isPoured)
        {
            Debug.Log("Tea has already been poured!");
            return;
        }

        if (container.CompareTag("Cup"))
        {
            cup = container;
            isPoured = true;
            Debug.Log("Tea poured into cup.");
        }
        else if (container.CompareTag("Glass"))
        {
            glass = container;
            isPoured = true;
            Debug.Log("Tea poured into glass.");
        }
        else
        {
            Debug.Log("Invalid container!");
        }
    }

    public void AddIce()
    {
        if (!isPoured || glass != null)
        {
            Debug.Log("Ice can only be added to a glass.");
            return;
        }

        hasIce = true;
        Debug.Log("Ice added to glass.");
    }
}