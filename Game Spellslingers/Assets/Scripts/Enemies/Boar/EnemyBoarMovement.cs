using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoarMovement : EnemyMovement
{
    private float chargeCD;
    private float chargeRange;
    private Transform player;
    private Vector3 playerDirection;
    private bool isCharging;

    public void Start()
    {
        isCharging = false;
        chargeRange = 12;
        chargeCD = 0;
        player = Player.instance.gameObject.transform;
    }

    public override void MoveToPlayer()
    {
        Vector3 direction = Player.instance.gameObject.transform.position - transform.position;
        direction.Normalize();

        if (!isCharging)
        {
            // stop moving when in range
            if (Vector2.Distance(transform.position, player.position) <= chargeRange && chargeCD <= 0)
            {

                anim.SetBool("Huff", true);
                SetX(0);
                SetY(0);

            }
            else if (Vector2.Distance(transform.position, player.position) > chargeRange || chargeCD > 0)
            {
                SetX(direction.x);
                SetY(direction.y);
                chargeCD -= Time.deltaTime;
            }
        }

    }

    public override void FixedUpdate()
    {
        if (!isCharging)
        {
            base.FixedUpdate();
        }
        else return;
    }

    private void Charge()
    {
        isCharging = true;
        this.playerDirection = Player.instance.transform.position - gameObject.transform.position;
        this.playerDirection.Normalize();
        SetMoveSpeed(0);
        rb.AddForce(this.playerDirection * 19, ForceMode2D.Impulse);
        anim.SetBool("Charge", true);
    }

    private void EndCharge()
    {
        anim.SetBool("Huff", false);
        anim.SetBool("Charge", false);
        this.rb.velocity = Vector3.zero;
        SetMoveSpeed(4);
        chargeCD = 6;
        isCharging= false;
    }

}
