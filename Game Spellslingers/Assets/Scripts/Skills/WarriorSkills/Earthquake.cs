using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : Skill
{
    public Earthquake() : base(1) { }
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Warrior warrior = (Warrior)Player.instance;
            warrior.IncreaseSlamArea(-2f);
            warrior.ActivateEarthquake();
        });
    }

    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return true;
    }
}
