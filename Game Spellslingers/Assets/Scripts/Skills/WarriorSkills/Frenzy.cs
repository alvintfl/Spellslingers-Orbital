using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frenzy : Skill
{
    public Frenzy() : base(1) { }
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Warrior warrior = (Warrior)Player.instance;
            
        });
    }
    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return true;
    }
}
