using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDialogueLauncher : MonoBehaviour
{    
    public delegate void DisplayDialogue(List<Dialogue> dialogues);
    public static event DisplayDialogue OnDisplayDialogue;
    public List<Dialogue> dialogues = new List<Dialogue>();

    public void LaunchDialogue()
    {
        OnDisplayDialogue.Invoke(dialogues);
    }
}
