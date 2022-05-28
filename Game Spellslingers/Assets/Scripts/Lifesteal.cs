using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that can heal the player
 * when they deal damage.
 * </summary>
 */
public class Lifesteal : MonoBehaviour
{
    private static float healAmount = 0f;

    public static void IncreaseHeal(float heal)
    {
        Lifesteal.healAmount += heal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Player.instance.Health.CurrentHealth += Lifesteal.healAmount;
        }
    }
}
