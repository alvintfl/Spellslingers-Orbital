using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCutter : StateMachineBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    private float timeBtwShots;
    public float startTimeBtwShots;

    private Transform player;
    private Vector2 target;

    private Transform cockatrice;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = Player.instance.gameObject.transform;
        target = new Vector2(player.position.x, player.position.y);
        timeBtwShots = 0.9f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timeBtwShots <= 0)
        {
            cockatrice = animator.GetComponent<Transform>();
            Vector3 diff = player.position - cockatrice.position;
            diff.Normalize();
            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            Instantiate(projectilePrefab, cockatrice.position, Quaternion.Euler(0f, 0f, angle));
            animator.SetBool("ClawCast", false);
            timeBtwShots = startTimeBtwShots;

            
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        

    }

}
