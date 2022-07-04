using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitality : Skill
{
    public Vitality() : base(10) { }
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Warrior warrior = (Warrior)Player.instance;
            int increase = 20;
            warrior.SetMaxHealth(warrior.GetMaxHealth() + increase);
            warrior.SetCurrentHealth(warrior.GetCurrentHealth() + increase);

            warrior.IncreaseRegen(1);
        });
    }

    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return false;
    }
}
