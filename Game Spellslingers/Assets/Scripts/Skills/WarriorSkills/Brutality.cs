using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brutality : Skill
{
    public Brutality() : base(5) { }
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Warrior warrior = (Warrior)Player.instance;
            warrior.SetAttack(warrior.GetAttack() + 15);
        });
    }

    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return false;
    }
}
