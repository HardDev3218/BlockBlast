using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPeace : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    
    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
