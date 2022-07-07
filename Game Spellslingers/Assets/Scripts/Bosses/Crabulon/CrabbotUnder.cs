using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabbotUnder : StateMachineBehaviour
{
    [SerializeField]
    private GameObject pincerGraspPrefab;
    private float randomOffsetX;
    private float randomOffsetY;

    private float attackNum = 0;

    private float startTimeBtwAttack = 1f;
    private float timeBtwAttack = 1f;

    private BoxCollider2D bodyCollider;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bodyCollider = animator.GetComponent<BoxCollider2D>();
        bodyCollider.enabled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timeBtwAttack <= 0 && attackNum < 6)
        {
            randomOffsetX = Random.Range(-5, 5);
            randomOffsetY = Random.Range(-5, 5);

            Vector2 targetLocation = new Vector2(Player.instance.transform.position.x + randomOffsetX, Player.instance.transform.position.y + randomOffsetY);
            Instantiate(pincerGraspPrefab, targetLocation, Quaternion.identity);
            AudioManager.instance.Play("CrabbotPincerGrasp");
            timeBtwAttack = startTimeBtwAttack;
            attackNum += 1;
        }
        else if (timeBtwAttack > 0 && attackNum < 6)
        {
            timeBtwAttack -= Time.deltaTime;
        }

        else
        {
            animator.SetBool("Under", false);
            bodyCollider.enabled = true;
            attackNum = 0;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Under", false);
    }
}
