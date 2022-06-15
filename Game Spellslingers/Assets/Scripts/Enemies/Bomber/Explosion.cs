using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that represents an explosion.
 * </summary>
 */
public class Explosion : MonoBehaviour
{
    private int damage = 30;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Player.instance.TakeDamage(this.damage);
        }
    }
}
