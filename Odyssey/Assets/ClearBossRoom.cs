using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClearBossRoom : MonoBehaviour
{
    private Room room;
    private bool hasClearedChapter = false;
    public UnityEvent OnDisplayClearChapterPanel;
    // Start is called before the first frame update
    void Awake()
    {
        room = GetComponent<Room>();
    }

    // Update is called once per frame
    void Update()
    {
        if (room.Cleared && !hasClearedChapter)
        {
            hasClearedChapter = true;
            OnDisplayClearChapterPanel.Invoke();
        }
    }
}
