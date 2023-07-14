using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClearBossRoom : MonoBehaviour
{
    public Room room;
    private bool hasClearedChapter = false;
    public UnityEvent OnDisplayClearChapterPanel;

    // Update is called once per frame
    void Update()
    {
        if (room.Cleared && !hasClearedChapter)
        {
            Debug.LogWarning("cleared chapter");
            hasClearedChapter = true;
            OnDisplayClearChapterPanel.Invoke();
        }
    }
}
