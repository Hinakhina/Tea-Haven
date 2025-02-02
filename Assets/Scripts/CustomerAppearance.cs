using UnityEngine;

public class CustomerAppearance : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] customerSprites;

    private void Start()
    {
        if (customerSprites.Length > 0 && spriteRenderer != null)
        {
            spriteRenderer.sprite = customerSprites[Random.Range(0, customerSprites.Length)];
        }
        spriteRenderer.enabled = true;
        Debug.Log("Customer sprite enabled: " + spriteRenderer.enabled);
    }

    private void OnEnable()
    {
        if (spriteRenderer != null && customerSprites.Length > 0)
        {
            spriteRenderer.sprite = customerSprites[Random.Range(0, customerSprites.Length)];
            spriteRenderer.enabled = true;
        }
    }

}
