using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterClearPanelActivation : MonoBehaviour
{
    private Room room;

    private void Start()
    {
        room = GetComponentInParent<Room>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("here");

        if (!room.Cleared) return;

        if (other.gameObject.tag == "Player")
        {
            GameObject.Find("UIManager").GetComponent<UIManager>()
                .DisplayChapterClearPanel();
        }
    }
}
