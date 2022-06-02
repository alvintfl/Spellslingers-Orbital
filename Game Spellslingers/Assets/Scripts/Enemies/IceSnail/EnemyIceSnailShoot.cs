using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIceSnailShoot : Shoot
{
    private WaitForSeconds wait;
    private int[,] directions;

    public override void Start()
    {
        base.Start();
        this.wait = new WaitForSeconds(2.5f);
        this.directions = new int[,]
        { {0,1}, {1,1}, {1,0}, {1,-1}, {0,-1}, {-1,-1}, {-1,0}, {-1,1} };
        StartCoroutine(Fire());
    }

    public override IEnumerator Fire()
    {
        while (true)
        {
            for (int i = 0; i < this.directions.GetLength(0); i++)
            {
                GameObject projectile = GetProjectile();
                if (projectile != null)
                {
                    // Position, direction and speed variables to fire projectile.
                    projectile.transform.position = gameObject.transform.position;
                    Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                    float speed = projectile.GetComponent<Projectile>().Speed;
                    Vector2 direction = new Vector2(
                        this.directions[i, 0],
                        this.directions[i, 1]);
                    projectile.transform.rotation = Quaternion.Euler(0, 0, i * -45);
                    direction.Normalize();

                    rb.AddForce(direction * speed, ForceMode2D.Impulse);
                }
            }
            yield return this.wait;
        }
    }
}
