using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIceSnailHealth : Health
{
    public override void TakeDamage(float damage)
    {
        AnimatorStateInfo state = Animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("Unhide") || state.IsName("Move"))
        {
            base.TakeDamage(damage);
        }
    }
}
