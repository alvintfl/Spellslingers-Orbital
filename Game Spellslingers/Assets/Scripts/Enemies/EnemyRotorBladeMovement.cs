using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotorBladeMovement : EnemyMovement
{
    private float bladeDistance;

    public void Start()
    {
        bladeDistance = 2;
    }
    public override void MoveToPlayer()
    {
        base.MoveToPlayer();
        if (Vector2.Distance(transform.position, Player.instance.gameObject.transform.position) <= bladeDistance)
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }
    }
}
