using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClearBossRoom : MonoBehaviour
{
    public Room room;
    public float PanelActivationDelay = 0.5f;
    private bool hasClearedChapter = false;
    //public UnityEvent OnDisplayClearChapterPanel;

    // Update is called once per frame
    void Update()
    {
        if (room.Cleared && !hasClearedChapter)
        {
            hasClearedChapter = true;
            Invoke("ActivatePanel", PanelActivationDelay);

            /*
            Debug.LogWarning("cleared chapter");
            hasClearedChapter = true;
            OnDisplayClearChapterPanel.Invoke();
            */
        }
    }

    private void ActivatePanel()
    {
        GameObject.Find("UIManager").GetComponent<UIManager>().DisplayChapterClearPanel();
    }
}
