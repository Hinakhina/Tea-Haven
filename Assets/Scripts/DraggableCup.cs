using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableCup : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform startParent;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
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
        EventTrigger trigger = gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { OnPointerClick((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger Entered: " + other.gameObject.name);
        if (other.CompareTag("Customer"))
        {
            Debug.Log("Cup given to customer: " + other.gameObject.name);
            other.GetComponent<Customer>()?.ReceiveOrder(this.gameObject);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Clicked on {gameObject.name}");
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
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
        );

        transform.localPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Cup Drag Ended at: " + transform.position);

        if (IsOverDropZone(eventData))
        {
            GameObject dropZone = GameObject.FindWithTag("DropZone");
            transform.position = dropZone.transform.position;
            Debug.Log("Cup successfully dropped on the drop zone!");
        }
        else
        {
            Debug.Log("Cup dropped outside the valid area, resetting position.");
            ResetPosition();
        }
    }
    private bool IsOverDropZone(PointerEventData eventData)
    {
        GameObject dropZone = GameObject.FindWithTag("DropZone");

        if (dropZone == null)
        {
            Debug.LogError("DropZone not found in the scene!");
            return false;
        }

        Vector2 dropZonePosition = dropZone.transform.position;
        float distance = Vector2.Distance(transform.position, dropZonePosition);

        Debug.Log($"Cup Position: {transform.position} | DropZone Position: {dropZonePosition} | Distance: {distance}");

        return distance < 50f;
    }
    private void ResetPosition()
    {
        transform.position = startPosition;
        transform.SetParent(startParent);
    }
}
