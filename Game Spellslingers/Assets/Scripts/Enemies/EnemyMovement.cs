using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/** 
 * <summary>
 * A class for enemies to manage
 * their movement.
 * </summary>
 */
public class EnemyMovement : Movement
{
    public override void Update()
    {
        MoveToPlayer();
        base.Update();
    }

    public virtual void MoveToPlayer()
    {
        Vector3 direction = Player.instance.gameObject.transform.position - transform.position;
        // for rotation
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        SetX(direction.x);
        SetY(direction.y);  
    }
}
