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
}
