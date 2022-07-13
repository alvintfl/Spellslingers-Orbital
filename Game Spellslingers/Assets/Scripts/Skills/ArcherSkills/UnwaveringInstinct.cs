using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnwaveringInstinct : Skill
{
    public UnwaveringInstinct() : base(1) { }
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Archer archer = (Archer)Player.instance;
            archer.ActivateUnwaveringInstinct();
        });
    }

    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return true;
    }
}
