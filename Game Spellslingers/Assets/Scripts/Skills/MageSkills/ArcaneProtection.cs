using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneProtection : Skill
{
    public ArcaneProtection() : base(1) { }
    
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Mage mage = (Mage)Player.instance;
            mage.CastArcaneShield();
        });
    }

    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return false;
    }
}
