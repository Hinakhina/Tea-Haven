using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private Transform originalParent;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalParent = transform.parent;
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(transform.root); // Move to top UI layer to avoid sorting issues
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // Check if dropped in a valid location
        if (!IsDroppedInValidTarget())
        {
            ReturnToOriginalPosition();
        }
    }

    private bool IsDroppedInValidTarget()
    {
        // Assuming a list of valid drop zones that should contain game objects
        GameObject dropTarget = GetDropTarget();
        if (dropTarget != null)
        {
            Debug.Log($"Dropped on: {dropTarget.name}");
            return true;
        }
        return false;
    }

    private GameObject GetDropTarget()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("DropZone")) // Ensure the object is a valid drop target
            {
                return result.gameObject;
            }
        }

        return null;
    }

    private void ReturnToOriginalPosition()
    {
        transform.SetParent(originalParent);
        rectTransform.anchoredPosition = originalPosition;
    }
}
