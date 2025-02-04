using UnityEngine;

[CreateAssetMenu(fileName = "NewTeaVariant", menuName = "Tea System/Tea Variant")]
public class TeaVariant : ScriptableObject
{
    public string teaName;
    public Sprite teaLeavesSprite;  // Sprite when tea leaves are added
    public Sprite brewingSprite;    // Sprite when tea is brewing
    public Sprite brewedSprite;     // Sprite after brewing is complete
}
