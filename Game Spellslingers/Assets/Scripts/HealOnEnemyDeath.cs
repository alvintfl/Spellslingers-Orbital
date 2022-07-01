using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOnEnemyDeath : MonoBehaviour
{
    private float healAmount = 0f;

    private void Start()
    {
        Enemy.EnemyDeath += Heal;
    }

    private void OnDestroy()
    {
        Enemy.EnemyDeath -= Heal;
    }

    private void Heal(Enemy sender, EventArgs e)
    {
        Player player = Player.instance;
        float newHealth = player.GetCurrentHealth() + this.healAmount;
        if (newHealth > player.GetMaxHealth())
        {
            player.SetCurrentHealth(player.GetMaxHealth());
        }
        else
        {
            player.SetCurrentHealth(newHealth);
        }
    }

    public void IncreaseHeal(float heal)
    {
        this.healAmount += heal;
    }

    public void Reset()
    {
        this.healAmount = 0f;
    }
}
