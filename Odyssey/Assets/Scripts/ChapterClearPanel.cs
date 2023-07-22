using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterClearPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Debug.Log("Panel is enabled");
        Debug.Log(PauseMenu.Instance);
        PauseMenu.Instance.Pause();
    }
}
