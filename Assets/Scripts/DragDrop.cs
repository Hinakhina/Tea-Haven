using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 originalPosition;
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Canvas canvas;
    private Button button;
    public List<Transform> goalObjects;

    private void Start()
    {
        button = GetComponent<Button>();
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (button != null)
            button.interactable = false;

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        originalPosition = transform.position;

        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
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
        if (button != null)
            button.interactable = true;

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        bool validDrop = false;

        foreach (Transform goal in goalObjects)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(
                goal.GetComponent<RectTransform>(),
                eventData.position,
                eventData.pressEventCamera))
            {
                validDrop = true;
                HandleDrop(goal.gameObject);
                break;
            }
        }

        if (!validDrop)
        {
            ResetPosition();
        }
    }

    private bool CheckOrderMatch(CustomerOrder customerOrder, DrinkContainer drink)
    {
        List<string> requiredIngredients = customerOrder.GetOrderIngredients();
        List<string> drinkIngredients = drink.GetIngredients();

        return new HashSet<string>(requiredIngredients).SetEquals(drinkIngredients);
    }
    private void HandleDrop(GameObject target)
    {
        if (target.CompareTag("Brewer"))
        {
            Brewer brewer = target.GetComponent<Brewer>();
            if (brewer != null)
            {
                TeaVariant teaVariant = GetTeaVariantFromDraggedObject();
                if (teaVariant != null)
                {
                    brewer.AddIngredients(teaVariant);
                }
                else if (CompareTag("Water"))
                {
                    brewer.AddWater();
                }
            }
        }
        else if (target.CompareTag("Cup") || target.CompareTag("Glass"))
        {
            Brewer brewer = GameObject.FindGameObjectWithTag("Brewer").GetComponent<Brewer>();
            if (brewer != null && brewer.isBrewed)
            {
                TeaVariant pouredTea = brewer.PourTea();
                if (pouredTea != null)
                {
                    DrinkContainer drinkContainer = target.GetComponent<DrinkContainer>();
                    if (drinkContainer != null)
                    {
                        drinkContainer.SetTeaVariant(pouredTea);
                        Debug.Log($"Tea poured into container: {pouredTea.teaName}");  // Debug log
                    }
                }
            }
        }
        else if (target.CompareTag("Customer"))
        {
            CustomerOrder customerOrder = target.GetComponent<CustomerOrder>();
            DrinkContainer drink = GetComponent<DrinkContainer>();

            if (customerOrder != null && drink != null)
            {
                bool isCorrect = CheckOrderMatch(customerOrder, drink);
                customerOrder.ReceiveTea(isCorrect);
                Destroy(drink.gameObject);
            }
        }

        ResetPosition();
    }

    private TeaVariant GetTeaVariantFromDraggedObject()
    {
        TeaIngredient variantRef = GetComponent<TeaIngredient>();
        return variantRef != null ? variantRef.teaVariant : null;
    }

    private void ResetPosition()
    {
        transform.position = originalPosition;
        transform.SetParent(originalParent);
    }
}