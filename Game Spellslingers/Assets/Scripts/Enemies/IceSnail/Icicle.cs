using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that represents the 
 * icicle projectile.
 * </summary>
 */
public class Icicle : Projectile
{
    private int damage;

    public Icicle() : base(10f, 250f) { }

    private void Start()
    {
        this.damage = 20;
    }

    public override int GetDamage()
    {
        return this.damage;
    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")) 
        {
            if (!Player.instance.Avoidance.avoidRoll())
            {
                Player.instance.Health.TakeDamage(this.damage);
            }
        }
        base.OnTriggerEnter2D(collider);
    }
}
