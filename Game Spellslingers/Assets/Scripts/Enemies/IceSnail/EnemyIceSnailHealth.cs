using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that manages the health 
 * of the ice snail.
 * </summary>
 */
public class EnemyIceSnailHealth : Health
{
    /**
     * <summary>
     * The ice snaill only takes damage 
     * when out of it's shell.
     * </summary>
     */
    public override void TakeDamage(float damage)
    {
        AnimatorStateInfo state = Animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("Unhide") || state.IsName("Move"))
        {
            base.TakeDamage(damage);
        }
    }
}
