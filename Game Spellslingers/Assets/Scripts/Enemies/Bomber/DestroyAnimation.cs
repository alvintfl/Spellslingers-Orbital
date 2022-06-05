using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that destroys the game object it is 
 * attached to once their animation finishes.
 * </summary>
 */
public class DestroyAnimation : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject, stateInfo.length);
    }
}
