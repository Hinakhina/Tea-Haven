using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private Vector3 originalPosition;
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private bool isFollowingCursor = false;

    public List<Transform> goalObjects;

    private void Start()
    {
        originalPosition = transform.position;
        originalParent = transform.parent;

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    private void Update()
    {
        if (isFollowingCursor)
        {
            transform.position = Input.mousePosition;

            if (Input.GetMouseButtonDown(1))
            {
                ResetPosition();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isFollowingCursor)
        {
            isFollowingCursor = true;
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            if (IsOverGoal())
            {
                Debug.Log("Ingredient placed!");
                ResetPosition();
            }
            else
            {
                Debug.Log("Invalid placement!");
                ResetPosition();
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isFollowingCursor = false;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (IsOverGoal())
        {
            Debug.Log("Ingredient placed via drag!");
            ResetPosition();
        }
        else
        {
            ResetPosition();
        }
    }

    private bool IsOverGoal()
    {
        foreach (Transform goal in goalObjects)
        {
            float distance = Vector3.Distance(transform.position, goal.position);
            if (distance < 50f)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetPosition()
    {
        transform.position = originalPosition;
        transform.SetParent(originalParent);
        isFollowingCursor = false;
        canvasGroup.blocksRaycasts = true;
    }
}
