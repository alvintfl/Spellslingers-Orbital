using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAshtoadMovement : EnemyMovement
{
    private float stoppingDistance;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private Transform player;

    [SerializeField]
    private GameObject projectilePrefab;

    public void Start()
    {
        stoppingDistance = 8;
        player = Player.instance.gameObject.transform;
        timeBtwShots = startTimeBtwShots;
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
            anim.SetBool("InRange", false);
            SetX(direction.x);
            SetY(direction.y);
        }

    }

    private void AttackPlayer() 
    {
        if (timeBtwShots <= 0)
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
