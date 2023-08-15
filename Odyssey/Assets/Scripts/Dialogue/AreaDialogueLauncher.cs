using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDialogueLauncher : MonoBehaviour
{
    public delegate void DisplayDialogue(List<Dialogue> dialogues);
    public static event DisplayDialogue OnDisplayDialogue;
    public List<Dialogue> dialogues = new List<Dialogue>();
    private bool hasDisplayed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (hasDisplayed) return;
        OnDisplayDialogue.Invoke(dialogues);
        hasDisplayed = true;
    }
}