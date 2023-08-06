using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MonkActivator : MonoBehaviour
{
    public bool IsNearMonk = false;
    public delegate void DisplayDialogue(List<Dialogue> dialogues);
    public static event DisplayDialogue OnDisplayDialogue;
    [SerializeField]
    private List<Dialogue> possibleTips = new List<Dialogue>();
    public List<Dialogue> tips = new List<Dialogue>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Debug.Log("in field");
        IsNearMonk = true;

        collision.transform.parent
            .GetComponentInChildren<KeyPressPromptPositionUpdate>()
            .PlayEAnimation(null);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        IsNearMonk = false;

        collision.transform.parent
            .GetComponentInChildren<KeyPressPromptPositionUpdate>()
            .StopPlayingAnimation();
    }
    public void OnTalkToMonk(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        if (!IsNearMonk)
        {
            return;
        }
        tips[0] = possibleTips[Random.Range(0, possibleTips.Count)];
        OnDisplayDialogue.Invoke(tips);
    }
}
