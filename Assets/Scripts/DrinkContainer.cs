using UnityEngine;

public class DrinkContainer : MonoBehaviour
{
    public bool isCold;
    public bool hasTea = false;
    public bool hasMilk = false;
    public bool hasSugar = false;

    public void AddTea()
    {
        if (!hasTea)
        {
            hasTea = true;
            Debug.Log(isCold ? "Iced tea poured into glass." : "Hot tea poured into cup.");
        }
        else
        {
            Debug.Log("This container already has tea!");
        }
    }

    public void AddMilk()
    {
        if (hasTea && !hasMilk)
        {
            hasMilk = true;
            Debug.Log("Milk added to the drink.");
        }
        else if (!hasTea)
        {
            Debug.Log("You can't add milk before tea!");
        }
    }

    public void AddSugar()
    {
        if (hasTea && !hasSugar)
        {
            hasSugar = true;
            Debug.Log("Sugar added to the drink.");
        }
        else if (!hasTea)
        {
            Debug.Log("You can't add sugar before tea!");
        }
    }
}
