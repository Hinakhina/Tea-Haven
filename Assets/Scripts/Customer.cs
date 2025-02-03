//using UnityEngine;
//using UnityEngine.UI;

//public class Customer : MonoBehaviour
//{
//    public string teaOrder;
//    public Text orderText; // UI text to show order

//    private void Start()
//    {
//        GenerateOrder();
//    }

//    private void GenerateOrder()
//    {
//        string[] teaOptions = { "Green Tea", "Black Tea", "Oolong Tea" };
//        teaOrder = teaOptions[Random.Range(0, teaOptions.Length)];
//        orderText.text = "Order: " + teaOrder;
//    }
//}