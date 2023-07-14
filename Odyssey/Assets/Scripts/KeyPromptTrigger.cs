using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeyPromptTrigger : MonoBehaviour
{
    public string KeyAnimationToPlay;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (OnCondition()) return;

        collision.transform.parent
            .GetComponentInChildren<KeyPressPromptPositionUpdate>()
            .DisplayKeyAnimation(KeyAnimationToPlay);
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        //if (!OnCondition()) return;

        collision.transform.parent
            .GetComponentInChildren<KeyPressPromptPositionUpdate>()
            .StopPlayingAnimation();
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (OnTerminateCondition()) gameObject.SetActive(false);
    }

    protected abstract bool OnCondition();
    protected abstract bool OnTerminateCondition();
}
