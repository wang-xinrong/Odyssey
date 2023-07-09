using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Changes the value of certain boolean variable when entering or exiting
// a animation state/state machine.
public class SetFloatBehaviour : StateMachineBehaviour
{
    public string FloatName;
    public bool UpdateOnStateEnter, UpdateOnStateExit;
    public bool UpdateOnStateMachineEnter, UpdateOnStateMachineExit;
    public float ValueOnEnter, ValueOnExit;



    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (UpdateOnStateEnter)
        {
            animator.SetFloat(FloatName, ValueOnEnter);
        }
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //
    //   
    //}

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (UpdateOnStateExit)
        {
            animator.SetFloat(FloatName, ValueOnExit);
        }
    }

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (UpdateOnStateMachineEnter)
        {
            animator.SetFloat(FloatName, ValueOnEnter);
        }
    }

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (UpdateOnStateMachineExit)
        {
            animator.SetFloat(FloatName, ValueOnExit);
        }
    }
}
