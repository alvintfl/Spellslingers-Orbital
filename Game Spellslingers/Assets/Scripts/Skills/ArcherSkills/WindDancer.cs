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
            Archer archer = (Archer) Player.instance;
            archer.SetAvoidChance(archer.GetAvoidChance() + 30);
            archer.SetRestoreOnAvoid(true);
        });
    }
    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return true;
    }
}
