    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCockatriceAI : EnemyMovement
{
    // distances between boss and player
    private float meleeDistance;
    private float stoppingDistance;
    private float resetDistance;

    // starting pos to reset boss
    private Vector2 startPos;
    private float prev;

    private Transform player;

    private int clawCastNum;
    private bool isCasting;

    void Start()
    {
        meleeDistance = 5;
        resetDistance = 50;
        stoppingDistance = 10;
        player = Player.instance.gameObject.transform;
        clawCastNum = 0;
        prev = 0;

        startPos = transform.position;
    }

    public override void MoveToPlayer()
    {
        Vector3 direction = Player.instance.gameObject.transform.position - transform.position;
        direction.Normalize();

        if (Vector2.Distance(transform.position, player.position) <= meleeDistance)
        {
            anim.SetBool("Aggro", true);
            anim.SetBool("InRange", true);
            anim.SetBool("InMeleeRange", true);
        }
        // stop moving when in range
        else if (Vector2.Distance(transform.position, player.position) <= stoppingDistance)
        {
            anim.SetBool("Aggro", true);
            anim.SetBool("InMeleeRange", false);
            anim.SetBool("InRange", true);
            SetX(0);
            SetY(0);
            AttackPlayer();

        }
        else if (resetDistance >= Vector2.Distance(transform.position, player.position) && Vector2.Distance(transform.position, player.position)  > stoppingDistance)
        {
            anim.SetBool("Aggro", true);
            anim.SetBool("InMeleeRange", false);
            anim.SetBool("InRange", false);
            SetX(direction.x);
            SetY(direction.y);
        }
        else 
        {
            anim.SetBool("Aggro", false);
            anim.SetBool("InRange", false);
            SetX(0);
            SetY(0);
            ResetBoss();
        }

    }

    

    private void AttackPlayer()
    {
        if (!isCasting)
        {
            // decide what attack to use
            this.isCasting = true;
            SetMoveSpeed(0);
            /*
             * Boss moveset
             * 0 - swipe attack (in melee distance only)
             * 1 - claw casting projectiles
             * 2 - wing flap summons tornadoes
             * 3 - mouth open breathing ice
             */
            float randomAttack = Random.Range(1, 3);
            while (randomAttack == prev)
            {
                randomAttack = Random.Range(1, 3);
            }
            if (randomAttack == 1)
            {
                anim.SetBool("ClawCast", true);
                prev = 1;
            }

            if (randomAttack == 2)
            {
                anim.SetBool("WingFlap", true);
                prev = 2;
            }

            if (randomAttack == 3)
            {
                anim.SetBool("OpenMouth", true);
                prev = 3;
            }
        }

    }

    public int GetClawCastNum()
    {
        return this.clawCastNum;
    }
    public void SetClawCastNum(int num)
    {
        this.clawCastNum = num;
    }

    private void ResetBoss()
    {
        this.transform.position = startPos;
        // reset health
    }

    private void AnimEnd()
    {
        SetMoveSpeed(2);
        this.isCasting = false;
    }
}
