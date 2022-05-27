using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifesteal : MonoBehaviour
{
    private static float healAmount = 0f;

    public static void IncreaseHealAmount()
    {
        Lifesteal.healAmount += 0.05f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Player.instance.Health.CurrentHealth += Lifesteal.healAmount;
        }
    }
}
