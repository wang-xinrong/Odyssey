using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteractionSoundEffect : MonoBehaviour
{
    public AudioClip PurchaseSound;

    public static AudioClip SuccessfulPurchaseSound;

    private void Awake()
    {
        SuccessfulPurchaseSound = PurchaseSound;
    }

    public static void PlayPurchaseSound()
    {
        AudioManager.Instance.PlaySound(SuccessfulPurchaseSound);
    }
}
