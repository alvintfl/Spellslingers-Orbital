using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOnEnemyKill : MonoBehaviour
{
    public float HealAmount { get; private set; }

    private void Awake()
    {
        this.HealAmount = 0f;
        gameObject.SetActive(false);
    }

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
        float newHealth = player.GetCurrentHealth() + this.HealAmount;
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
        this.HealAmount += heal;
    }
}
