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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (this.enabled && collider.gameObject.CompareTag("Enemy"))
        {
            Player player = Player.instance;
            float newHealth = player.GetCurrentHealth() + Lifesteal.healAmount;
            if (newHealth > player.GetMaxHealth())
            {
                player.SetCurrentHealth(player.GetMaxHealth());
            }
            else
            {
                player.SetCurrentHealth(newHealth);
            }
        }
    }
}
