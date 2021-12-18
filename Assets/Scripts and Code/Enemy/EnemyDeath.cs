using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<EnemyMovement>().enabled = false;

        Rigidbody2D rb = animator.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject);
    }
}
