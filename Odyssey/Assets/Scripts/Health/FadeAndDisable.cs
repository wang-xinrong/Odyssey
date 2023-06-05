using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Fades away the object attached to and disable it
// after a pre-determined period of time.
public class FadeAndDisable : StateMachineBehaviour
{
    // time before an object is faded off
    public float FadeTime = 0.5f;
    private float _timeElapsed = 0;
    private SpriteRenderer _sr;
    private GameObject _objToDisable;
    private Color _StartColor;
    private float _newAlpha;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timeElapsed = 0f;
        _sr = animator.GetComponent<SpriteRenderer>();
        _objToDisable = animator.gameObject;
        _StartColor = _sr.color;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timeElapsed += Time.deltaTime;
        // opacity factor, which gradually reduces as time lapses
        _newAlpha = _StartColor.a * (1 - _timeElapsed / FadeTime);
        _sr.color = new Color(_StartColor.r, _StartColor.g, _StartColor.b, _newAlpha);

        if (_timeElapsed > FadeTime)
        {
            _objToDisable.SetActive(false);
        }
    }
}
