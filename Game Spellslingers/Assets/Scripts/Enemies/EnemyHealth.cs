using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public static int maxHealth = 50;

    public EnemyHealth() : base(EnemyHealth.maxHealth) { }
}
