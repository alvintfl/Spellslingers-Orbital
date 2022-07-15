using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySalamanderMovement : EnemyMovement
{
    private float stoppingDistance;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private Transform player;
    private bool isAttacking;

    [SerializeField]
    private GameObject projectilePrefab;

    private float xDiff;

    private SpriteRenderer sr;

    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        stoppingDistance = 10;
        player = Player.instance.gameObject.transform;
        timeBtwShots = startTimeBtwShots;
        isAttacking = false;
    }

    public override void MoveToPlayer()
    {
        Vector3 direction = Player.instance.gameObject.transform.position - transform.position;
        direction.Normalize();

        // stop moving when in range
        if (Vector2.Distance(transform.position, player.position) <= stoppingDistance)
        {
            anim.SetBool("InRange", true);
            SetX(0);
            SetY(0);
            AttackPlayer();

        }
        else if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            isAttacking = false;
            anim.SetBool("InRange", false);
            SetX(direction.x);
            SetY(direction.y);
        }

    }

    private void AttackPlayer()
    {
        isAttacking = true;
        if (timeBtwShots <= 0)
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            timeBtwShots = 5;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public override void AnimateRotation()
    {
        if (isAttacking)
        {
            if (player.position.x - transform.position.x > 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                gameObject.transform.rotation = Quaternion.Euler(0, -180, 0);
            }
        }
        else base.AnimateRotation();

    }
}
