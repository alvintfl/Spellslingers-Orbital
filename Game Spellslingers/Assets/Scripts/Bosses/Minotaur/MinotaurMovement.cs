using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * <summary>
 * A class for Minotaur to manage
 * it's movement.
 * </summary>
 */
public class MinotaurMovement : Movement
{
    public override void Update()
    {
        MoveToPlayer();
        base.Update();
    }

    public void MoveToPlayer()
    {
        Vector3 direction = Player.instance.gameObject.transform.position - transform.position;
        direction.Normalize();
        SetX(direction.x);
        SetY(direction.y);  
    }
}
