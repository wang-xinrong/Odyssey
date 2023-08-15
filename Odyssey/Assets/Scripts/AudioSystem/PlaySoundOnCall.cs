using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCall : MonoBehaviour
{
    [SerializeField] private AudioClip BasicAttackClip;
    [SerializeField] private AudioClip SpecialAttackClip;

    public void PlayBasicAttackSound()
    {
        AudioManager.Instance.PlaySound(BasicAttackClip);
    }

    public void PlaySpecialAttackSound()
    {
        AudioManager.Instance.PlaySound(SpecialAttackClip);
    }
}
