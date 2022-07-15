using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySnowThingAI : EnemyMovement
{
    private Transform player;
    private float stoppingDistance;
    private bool moving;

    // Start is called before the first frame update
    void Start()
    {
        stoppingDistance = 2;
        player = Player.instance.gameObject.transform;
    }

    public override void MoveToPlayer()
    {
        Vector3 direction = Player.instance.gameObject.transform.position - transform.position;
        direction.Normalize();

        // stop moving when in range and not in retreat distance
        if (Vector2.Distance(transform.position, player.position) <= stoppingDistance)
        {
            AudioManager.instance.Play("SnowCrunch");
            anim.SetBool("InRange", true);
            SetX(0);
            SetY(0);
        }
        else if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            anim.SetBool("InRange", false);
            SetX(direction.x);
            SetY(direction.y);
        }
    }

    private void SetMoveZero()
    {
        SetMoveSpeed(0);
    }

    private void SetMoveOriginal()
    {
        SetMoveSpeed(8);
    }
}
