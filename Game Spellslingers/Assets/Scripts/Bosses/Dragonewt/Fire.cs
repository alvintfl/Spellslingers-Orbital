using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Projectile
{
    private int damage;

    public Fire() : base(10f, 500f) { }

    private void Start()
    {
        this.damage = 5;
    }

    public override int GetDamage()
    {
        return this.damage;
    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")) 
        {
            Player.instance.TakeDamage(this.damage);
        }
        base.OnTriggerEnter2D(collider);
    }
}
