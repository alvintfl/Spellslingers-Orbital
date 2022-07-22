using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDAMove : EnemyMovement
{
    private float stoppingDistance;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private Transform player;
    private Transform da;
    private Animator anim;

    [SerializeField]
    private GameObject projectilePrefab;

    public void Start()
    {
        stoppingDistance = 8;
        player = Player.instance.gameObject.transform;
        timeBtwShots = startTimeBtwShots;
        anim = GetComponent<Animator>();
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
            da = anim.GetComponent<Transform>();
            Vector3 diff = player.position - da.position;
            diff.Normalize();
            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0f, 0f, angle));
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
