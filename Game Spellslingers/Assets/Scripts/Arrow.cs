using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    private static int damage = 10;
    public Arrow() : base(15f) { }

    public override void IncreaseDamage(int damage)
    {
        Arrow.damage += damage;
    }

    public override int GetDamage()
    {
        return Arrow.damage;
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
           base.OnCollisionEnter2D(collision);
           EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
           enemyHealth.TakeDamage(GetDamage());
           Debug.Log(enemyHealth.CurrentHealth);
        }
    }
}
