using UnityEngine;

public class ImageObjectWorldSpace : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
