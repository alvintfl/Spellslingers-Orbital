using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : Skill
{
    public Impact() : base(5) { }
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Warrior warrior = (Warrior)Player.instance;
            warrior.IncreaseSlamArea(0.5f);
        });
    }

    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return false;
    }
}
