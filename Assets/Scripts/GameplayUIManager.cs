using UnityEngine;
using TMPro;

public class GameplayUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private GameObject teaBrewingPanel;

    public void UpdateStatusText(string message)
    {
        if (statusText != null)
        {
            statusText.text = message;
        }
    }

    public void ShowTeaBrewingPanel(string tea)
    {
        if (teaBrewingPanel != null)
        {
            teaBrewingPanel.SetActive(true);
            statusText.text = tea;
        }
    }

    public void HideTeaBrewingPanel()
    {
        if (teaBrewingPanel != null)
        {
            teaBrewingPanel.SetActive(false);
        }
    }
}
