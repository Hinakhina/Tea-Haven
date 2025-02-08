using UnityEngine;
using UnityEngine.EventSystems;

public class Customer : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop triggered!");
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null)
        {
            Debug.Log($"Customer received {droppedObject.name}!");
            DraggableCup draggable = droppedObject.GetComponent<DraggableCup>();
            if (draggable != null)
            {
                Debug.Log("Cup is draggable, giving to customer...");
                ReceiveOrder(droppedObject);
            }
        }
    }

    public void ReceiveOrder(GameObject cup)
    {
        Debug.Log("Customer received: " + cup.name);
        Destroy(cup);
    }
}
