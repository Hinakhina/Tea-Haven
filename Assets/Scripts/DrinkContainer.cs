using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrinkContainer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool isCold;
    public bool hasTea = false;
    public bool hasMilk = false;
    public bool hasSugar = false;

    private Vector3 originalPosition;
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Canvas canvas;
    private string teaName;
    private List<string> ingredients = new List<string>();

    private void Start()
    {
        originalPosition = transform.position;
        originalParent = transform.parent;

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void SetTea(string tea)
    {
        teaName = tea;
        ingredients.Add(tea);
    }

    public void SetTeaVariant(TeaVariant variant)
    {
        TeaVariant teaVariant = variant;
    }

    public void AddIngredient(string ingredient)
    {
        if (!ingredients.Contains(ingredient))
        {
            ingredients.Add(ingredient);
        }
    }

    public List<string> GetIngredients()
    {
        return new List<string>(ingredients);
    }

    public void AddTea()
    {
        if (!hasTea)
        {
            hasTea = true;
            Debug.Log(isCold ? "Iced tea poured into glass." : "Hot tea poured into cup.");
        }
    }

    public void AddMilk()
    {
        if (hasTea && !hasMilk)
        {
            hasMilk = true;
            Debug.Log("Milk added to the drink.");
        }
    }

    public void AddSugar()
    {
        if (hasTea && !hasSugar)
        {
            hasSugar = true;
            Debug.Log("Sugar added to the drink.");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!hasTea) return;

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        originalPosition = transform.position;
        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!hasTea) return;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out localPoint
        );

        rectTransform.anchoredPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        GameObject hitObject = GetObjectUnderPointer(eventData);
        if (hitObject != null && hitObject.CompareTag("Customer"))
        {
            CustomerOrder customerOrder = hitObject.GetComponent<CustomerOrder>();
            if (customerOrder != null)
            {
                bool isCorrectOrder = CheckOrderMatch(customerOrder);
                customerOrder.ReceiveTea(isCorrectOrder);
            }
        }

        ResetPosition();
    }

    private GameObject GetObjectUnderPointer(PointerEventData eventData)
    {
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject != gameObject)
            {
                return result.gameObject;
            }
        }

        return null;
    }

    private bool CheckOrderMatch(CustomerOrder customerOrder)
    {
        List<string> orderIngredients = customerOrder.GetOrderIngredients();
        List<string> currentIngredients = new List<string>();

        if (hasTea) currentIngredients.Add(isCold ? "Ice" : "Hot Water");
        if (hasMilk) currentIngredients.Add("Milk");
        if (hasSugar) currentIngredients.Add("Sugar");

        if (currentIngredients.Count != orderIngredients.Count) return false;

        foreach (string ingredient in orderIngredients)
        {
            if (!currentIngredients.Contains(ingredient)) return false;
        }

        return true;
    }

    private void ResetPosition()
    {
        transform.position = originalPosition;
        transform.SetParent(originalParent);
    }
}