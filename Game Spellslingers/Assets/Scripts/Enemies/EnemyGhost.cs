using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhost : Enemy
{
    EnemyGhost() : base(10.5f) { }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("ghost has collided with player");
        if (collision.gameObject.CompareTag("Player"))
        {
            print(getEnemyDamage());
            Player.instance.Health.TakeDamage(getEnemyDamage());
        }
    }
}
