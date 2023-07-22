using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKAttackSpritePatch : MonoBehaviour
{
    public GameObject[] spriteRenderers;

    public void DeactivateSprites()
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].SetActive(false);
        }
    }
}
