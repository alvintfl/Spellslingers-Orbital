using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    private static int damage = 10;
    private static int pierceMax = 0;
    
    public static int getPierceMax()
    {
        return pierceMax;
    }
    public static void setPierceMax(int value)
    {
        pierceMax = value;
    }


    private int pierceCount = 0;
    public Arrow() : base(15f) { }

    public override void IncreaseDamage(int damage)
    {
        Arrow.damage += damage;
    }

    public override int GetDamage()
    {
        return Arrow.damage;
    }



    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (pierceCount >= pierceMax)
            {
                pierceCount = 0;
                base.OnTriggerEnter2D(collision);
                if (collision.gameObject != null)
                {
                    Health enemyHealth = collision.gameObject.GetComponent<Health>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(GetDamage());
                    }
                }
            }
            else
            {
                pierceCount += 1;
                if (collision.gameObject != null)
                {
                    Health enemyHealth = collision.gameObject.GetComponent<Health>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(GetDamage());
                    }
                }
            }
        }
    }

    public override void AtMaxRange()
    {
        if (this.firePoint != null && gameObject.activeSelf &&
                (Math.Abs(gameObject.transform.position.x - base.firePoint.position.x) > base.maxRange ||
                 Math.Abs(gameObject.transform.position.y - base.firePoint.position.y) > base.maxRange))
        {
            pierceCount = 0;
            OnCollided(EventArgs.Empty);
        }
    }
}
