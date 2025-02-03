using UnityEngine;

public class CustomerAppearance : MonoBehaviour
{

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] public Sprite[] customerSprites;

    private void Start()
    {
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is missing on: " + gameObject.name);
        }
        else if (customerSprites.Length == 0)
        {
            Debug.LogError("No customer sprites assigned!");
        }
        else
        {
            spriteRenderer.sprite = customerSprites[Random.Range(0, customerSprites.Length)];
            Debug.Log("Assigned sprite: " + spriteRenderer.sprite.name);
        }
    }

    private void OnEnable()
    {
        if (spriteRenderer != null && customerSprites.Length > 0)
        {
            spriteRenderer.sprite = null;
            spriteRenderer.sprite = customerSprites[Random.Range(0, customerSprites.Length)];
            spriteRenderer.enabled = true;
        }
    }

}
