using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crabbot_ShootBullets : StateMachineBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    private float timeBtwShots;
    public float startTimeBtwShots;
    private float projCount;

    private Transform player;
    private Vector2 target;

    private Transform crabbot;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = Player.instance.gameObject.transform;
        target = new Vector2(player.position.x, player.position.y);
        timeBtwShots = 0.2f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timeBtwShots <= 0 && projCount < 25)
        {
            crabbot = animator.GetComponent<Transform>();
            Vector3 diff = player.position - crabbot.position;
            diff.Normalize();
            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            Instantiate(projectilePrefab, crabbot.position, Quaternion.Euler(0f, 0f, angle));
            timeBtwShots = startTimeBtwShots;
            projCount += 1;
            AudioManager.instance.Play("CrabbotBulletRain");
        }
        else if (timeBtwShots > 0 && projCount < 25)
        {
            timeBtwShots -= Time.deltaTime;
        }
        else 
        {
            animator.SetBool("Bullets", false);
            animator.SetBool("EyeGlow", false);
            animator.GetComponent<BossCrabbot>().AnimEnd();
            projCount = 0;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Bullets", false);
        animator.SetBool("EyeGlow", false);
    }

}
