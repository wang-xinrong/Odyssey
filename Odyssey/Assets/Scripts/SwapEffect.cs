using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapEffect : MonoBehaviour
{
    public void ActivateSwapSmoke(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
        ActionSoundEffects.PlaySwap();
    }

    public void DeactivateSwapSmoke()
    {
        gameObject.SetActive(false);
    }
}
