using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindDancer : Skill
{
    public WindDancer() : base(1) { }
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Health playerHealth = Player.instance.Health;

            Avoidance playerAvoidance = Player.instance.Avoidance;
            playerAvoidance.SetAvoidChance(playerAvoidance.GetAvoidChance() + 30);
            playerAvoidance.SetRestoreOnAvoid(true);
        });
    }
    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return true;
    }
}
