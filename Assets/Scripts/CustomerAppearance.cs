using UnityEngine;

public class CustomerAppearance : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer; // Drag the SpriteRenderer here
    [SerializeField] private Sprite[] customerSprites; // Drag multiple sprites here

    private void Start()
    {
        if (customerSprites.Length > 0 && spriteRenderer != null)
        {
            spriteRenderer.sprite = customerSprites[Random.Range(0, customerSprites.Length)];
        }
    }
}
