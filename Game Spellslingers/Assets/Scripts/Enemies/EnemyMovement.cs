using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * <summary>
 * A class for enemies to manage
 * their movement.
 * </summary>
 */
public class EnemyMovement : Movement
{
    public Animator anim;
    public override void Awake()
    {
        base.Awake();
        this.anim = GetComponent<Animator>();
    }

    public override void Update()
    {
        MoveToPlayer();
        base.Update();
    }

    public virtual void MoveToPlayer()
    {
        /*
        if (GetMoveSpeed() == 0)
        {
            SetX(0);
            SetY(0);
        } else
        */
        {
            Vector3 direction = Player.instance.gameObject.transform.position - transform.position;
            direction.Normalize();
            SetX(direction.x);
            SetY(direction.y);  
        }
    }

    public override void AnimateMovement()
    {
        this.anim.SetFloat("Speed", GetMoveSpeed());
    }
}
