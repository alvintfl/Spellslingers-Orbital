using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Executioner : Skill
{
    public Executioner() : base(1) { }
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Warrior warrior = (Warrior)Player.instance;
            warrior.ActivateCull();
        });
    }
    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return true;
    }
}
