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
        //OnDisplayDialogue.Invoke(dialogues);
        StartCoroutine(StartDisplayDialogue(dialogues));
    }

    private IEnumerator StartDisplayDialogue(List<Dialogue> dialogues)
    {
        yield return new WaitForSeconds(.2f);
        OnDisplayDialogue.Invoke(dialogues);
    }
}
