using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entrench : Skill
{
    public Entrench() : base(5) { }
    public override void Start()
    { 
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Health playerHealth = Player.instance.Health;
            int increase = 20;
            playerHealth.MaxHealth += increase;
            playerHealth.CurrentHealth += increase;

            Avoidance playerAvoidance = Player.instance.Avoidance;
            playerAvoidance.setAvoidChance(playerAvoidance.getAvoidChance() + 3);
        });
    }
}
