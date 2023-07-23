using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKAttackSpritePatch : MonoBehaviour
{
    public SpriteRenderer[] spriteRenderers;
    public Collider2D[] colliders;

    public void DeactivateSprites()
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].enabled = false;
            colliders[i].enabled = false;
        }
    }
}
