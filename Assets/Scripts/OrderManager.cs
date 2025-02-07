using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public enum OrderType { HotTea, IcedTea }
    private ClockTimeRun clock;

    public struct Order
    {
        public OrderType Type;
        public bool NeedsSugar;
        public bool NeedsMilk;
        public bool NeedsIce;
    }

    public static OrderManager Instance;
    private Order currentOrder;
    private bool teaLeavesAdded = false;
    private bool waterAdded = false;
    private bool brewingComplete = false;
    private bool pouredIntoCup = false;
    private bool pouredIntoGlass = false;
    private bool sugarOrMilkAdded = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GenerateNewOrder();
    }

    public void GenerateNewOrder()
    {
        ResetOrderProgress();
        currentOrder = new Order
        {
            Type = (Random.value > 0.5f) ? OrderType.HotTea : OrderType.IcedTea,
            NeedsSugar = (Random.value > 0.5f),
            NeedsMilk = (Random.value > 0.5f),
            NeedsIce = (Random.value > 0.5f)
        };

        Debug.Log($"New Order: {currentOrder.Type}. Needs Sugar: {currentOrder.NeedsSugar}, Milk: {currentOrder.NeedsMilk}, Ice: {currentOrder.NeedsIce}");
    }

    public Order GetCurrentOrder()
    {
        return currentOrder;
    }

    public bool AddTeaLeaves()
    {
        if (!teaLeavesAdded)
        {
            teaLeavesAdded = true;
            Debug.Log("Tea leaves added!");
            return true;
        }
        Debug.Log("Tea leaves already added!");
        return false;
    }

    public bool AddWater()
    {
        if (teaLeavesAdded && !waterAdded)
        {
            waterAdded = true;
            Debug.Log("Water added! Start brewing...");
            return true;
        }
        Debug.Log("Add tea leaves first!");
        return false;
    }

    public void BrewingComplete()
    {
        if (teaLeavesAdded && waterAdded)
        {
            brewingComplete = true;
            Debug.Log("Brewing complete!");
        }
    }

    public bool PourIntoCup()
    {
        if (brewingComplete && currentOrder.Type == OrderType.HotTea && !pouredIntoCup)
        {
            pouredIntoCup = true;
            Debug.Log("Tea poured into a cup!");
            return true;
        }
        Debug.Log("Cannot pour into cup yet!");
        return false;
    }

    public bool PourIntoGlass()
    {
        if (brewingComplete && currentOrder.Type == OrderType.IcedTea && !pouredIntoGlass)
        {
            pouredIntoGlass = true;
            Debug.Log("Tea poured into a glass!");
            return true;
        }
        Debug.Log("Cannot pour into glass yet!");
        return false;
    }

    public bool AddSugarOrMilk()
    {
        if ((pouredIntoCup || pouredIntoGlass) && (currentOrder.NeedsSugar || currentOrder.NeedsMilk ) && !sugarOrMilkAdded)
        {
            sugarOrMilkAdded = true;
            Debug.Log("Sugar/Milk added!");
            return true;
        }
        Debug.Log("Cannot add sugar/milk yet!");
        return false;
    }

    public bool ServeOrder()
    {
        if ((pouredIntoCup || pouredIntoGlass) && ((!currentOrder.NeedsSugar || !currentOrder.NeedsMilk) || sugarOrMilkAdded))
        {
            Debug.Log("Order served! Generating new order...");
            GenerateNewOrder();
            return true;
        }
        Debug.Log("Cannot serve order yet!");
        return false;
    }

    private void ResetOrderProgress()
    {
        teaLeavesAdded = false;
        waterAdded = false;
        brewingComplete = false;
        pouredIntoCup = false;
        pouredIntoGlass = false;
        sugarOrMilkAdded = false;
    }
}