using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLauncher : MonoBehaviour
{
    public delegate void DisplayDialogue(List<Dialogue> dialogues);
    public static event DisplayDialogue OnDisplayDialogue;
    public Room room; 
    public List<Dialogue> dialogues = new List<Dialogue>();
    private bool hasDisplayed = false;

    void Start()
    {
        room = GetComponent<Room>();
    }
    void Update()
    {
        if (!room.Reached)
        {
            return;
        }
        if (hasDisplayed)
        {
            return;
        }
        if (OnDisplayDialogue == null)
        {
            return;
        }
        hasDisplayed = true;
        OnDisplayDialogue.Invoke(dialogues);
    }
}
