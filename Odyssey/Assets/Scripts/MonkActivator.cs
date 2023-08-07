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

    // to disable keyprompt when the dialogue box is active
    // and enable it back when the dialogue is disabled
    public GameObject DialogueBox;
    private KeyPressPromptPositionUpdate keyPrompt;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Debug.Log("in field");
        IsNearMonk = true;

        keyPrompt = collision.transform.parent
            .GetComponentInChildren<KeyPressPromptPositionUpdate>();

        keyPrompt.PlayEAnimation(null);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        IsNearMonk = false;

        keyPrompt = collision.transform.parent
            .GetComponentInChildren<KeyPressPromptPositionUpdate>();

        keyPrompt.StopPlayingAnimation();
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
        ToggleKeyprompt();
    }

    public void ToggleKeyprompt()
    {
        bool DialogueOn = DialogueBox.activeSelf;
        Debug.Log(DialogueOn);
        keyPrompt.GetComponent<SpriteRenderer>().enabled = !DialogueOn;
        /*
        bool IsActive = DialogueBox.activeSelf;
        keyPrompt.gameObject.SetActive(!IsActive);
        */
    }
}
