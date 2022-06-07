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

    public static void Reset()
    {
        Lifesteal.healAmount = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.enabled && collision.gameObject.CompareTag("Enemy"))
        {
            Player.instance.Health.CurrentHealth += Lifesteal.healAmount;
        }
    }
}
