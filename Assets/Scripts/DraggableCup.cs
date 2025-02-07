using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableCup : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform startParent;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas not found! Make sure you have a Canvas in your scene.");
        }
        canvasGroup = GetComponent<CanvasGroup>(); 
        if (canvasGroup == null)
        {
            Debug.LogWarning("CanvasGroup not found on the cup. Adding one now.");
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        startParent = transform.parent;
        transform.SetParent(canvas.transform, true);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(startParent, true);
        canvasGroup.blocksRaycasts = true;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(eventData.position), Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("Customer"))
        {
            hit.collider.GetComponent<CustomerOrder>().ReceiveTea(true);
            Destroy(gameObject);
        }
        else
        {
            transform.position = startPosition;
        }
    }
}
