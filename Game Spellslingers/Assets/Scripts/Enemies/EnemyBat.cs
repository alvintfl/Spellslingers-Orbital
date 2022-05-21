using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy
{
    EnemyBat() : base(5.5f) { }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bat has collided with player");
        if (collision.gameObject.CompareTag("Player"))
        {
            print(getEnemyDamage());
            Player.instance.TakeDamage(getEnemyDamage());
            Player.instance.CheckPlayerStatus();
        }
    }
}
