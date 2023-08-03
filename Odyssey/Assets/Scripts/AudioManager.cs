using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    private AudioSource _startMenuBGMSource, _effectSource
        , _townBGMSource, _chapterBGMSource, _tutorialBGMSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip audioClip)
    {
        _effectSource.PlayOneShot(audioClip);
    }

    
    public void StopPlaySound()
    {
        _effectSource.Stop();
    }
    

    public void PlayBGM(string location)
    {
        SilentAllBGMSources();
        switch (location)
        {
            case "StartMenu":
                _startMenuBGMSource.Play();
                Debug.Log("here");
                break;

            case "Town":
                _townBGMSource.Play();
                break;

            case "Chapter":
                _chapterBGMSource.Play();
                break;

            case "Tutorial":
                _tutorialBGMSource.Play();
                break;
        }
    }

    private void SilentAllBGMSources()
    {
        _startMenuBGMSource.Stop();
        _townBGMSource.Stop();
        _chapterBGMSource.Stop();
        _tutorialBGMSource.Stop();
    }
}
