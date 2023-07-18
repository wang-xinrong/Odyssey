using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    public bool IsBGM = false;
    public string Location;

    // Start is called before the first frame update
    void Start()
    {
        if (IsBGM)
        {
            AudioManager.Instance.PlayBGM(Location);
            return;
        }
        AudioManager.Instance.PlaySound(clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
