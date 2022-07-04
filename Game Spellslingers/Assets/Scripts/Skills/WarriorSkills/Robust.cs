using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robust : Skill
{
    public Robust() : base(5) { }
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Warrior warrior = (Warrior)Player.instance;
           
            warrior.IncreaseRegen(2);
        });
    }

    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return false;
    }
}
