using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleRain : StateMachineBehaviour
{
    [SerializeField]
    private GameObject icicleRainPrefab;

    private Camera camera;

    private float timeBtwIcicles;
    public float startTimeBtwIcicles;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeBtwIcicles = 0.6f;
        camera = Camera.main;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        {
            if (timeBtwIcicles <= 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    int icicleX = i * Screen.width / 8 + 200 + i * 10;
                    Vector2 coords = new Vector2(icicleX, Screen.height + 300);
                    Vector2 rainLocation = camera.ScreenToWorldPoint(coords);
                    Instantiate(icicleRainPrefab, rainLocation, Quaternion.identity);
                    animator.SetBool("WingFlap", false);

                }
                timeBtwIcicles = startTimeBtwIcicles;
            }
            else
            {
                timeBtwIcicles -= Time.deltaTime;
            }
        }

        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
