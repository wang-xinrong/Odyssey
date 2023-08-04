using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnEnable : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    public void Play()
    {
        AudioManager.Instance.PlaySound(clip);
    }

    /*

    public void OnDisable()
    {
        //AudioManager.Instance.StopPlaySound();
    }
    */
}
