using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableHiraganaLine : StateMachineBehaviour
{
    HiraganaFadeAnimation hfa;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hfa = animator.GetComponent<HiraganaFadeAnimation>();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hfa.otherHiraganaObject.SetActive(true);
        animator.gameObject.SetActive(false);
    }
}
