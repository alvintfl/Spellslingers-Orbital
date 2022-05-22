using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vigor : Skill
{
    public Vigor() : base(10) { }
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
        });
    }
}
