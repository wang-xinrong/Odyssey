using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Fades away the object attached to and disable it
// after a pre-determined period of time.
public class BossFadeAndDisable : FadeAndDisable
{
    public bool IsLastStage = false;

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timeElapsed += Time.deltaTime;
        // opacity factor, which gradually reduces as time lapses
        _newAlpha = _StartColor.a * (1 - _timeElapsed / FadeTime);
        _sr.color = new Color(_StartColor.r, _StartColor.g, _StartColor.b, _newAlpha);

        if (_timeElapsed > FadeTime && IsLastStage)
        {
            _objToDisable.SetActive(false);
        }
    }
}
