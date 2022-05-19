using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private HealthBar healthbar;
    [SerializeField] private Player player;

    private void Start()
    {
        this.healthbar.SetMaxHealth(Player.maxHealth);
        this.healthbar.SetHealth(Player.maxHealth);
        this.player.HealthChange += UpdateHealth;
    }

    public void UpdateHealth(object sender, EventArgs e)
    {
        this.healthbar.SetMaxHealth(Player.maxHealth);
        this.healthbar.SetHealth(player.CurrentHealth);
    }
}
