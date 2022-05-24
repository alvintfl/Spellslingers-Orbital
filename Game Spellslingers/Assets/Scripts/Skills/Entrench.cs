using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entrench : Skill
{
    public Entrench() : base(10) { }
    public override void Start()
    { 
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Health playerHealth = PlayerObject.GetComponent<Player>().Health;
            int increase = 20;
            playerHealth.MaxHealth += increase;
            playerHealth.CurrentHealth += increase;

            Avoidance playerAvoidance = PlayerObject.GetComponent<Player>().Avoidance;
            playerAvoidance.setAvoidChance(playerAvoidance.getAvoidChance() + 3);
        });
    }
}
