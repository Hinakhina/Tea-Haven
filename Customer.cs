using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public string Name;
    public CustomerOrder Order;

    public void PlaceOrder(string teaType, List<string> addOns)
    {
        Order = new CustomerOrder { TeaType = teaType, AddOns = addOns };
        Debug.Log($"{Name} ordered {teaType} with {string.Join(", ", addOns)}");
    }

    public void ServeTea()
    {
        Debug.Log($"{Name} is happy with their tea!");
    }

    public void Complain()
    {
        Debug.Log($"{Name} is unhappy. Wrong order!");
    }
}