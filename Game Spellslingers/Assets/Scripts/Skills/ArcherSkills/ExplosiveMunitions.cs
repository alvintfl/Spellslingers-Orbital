using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveMunitions : Skill
{
    public ExplosiveMunitions() : base(1) { }

    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Arrow.ActivateExplosion();
        });
    }
    public override void Reset()
    {
        Arrow.DeactivateExplosion();
    }

    public override bool IsSignatureSkill()
    {
        return true;
    }
}
