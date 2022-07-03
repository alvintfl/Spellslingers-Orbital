using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcanePower : Skill
{
    public ArcanePower() : base(10) { }
    
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Mage mage = (Mage) Player.instance;
            mage.IncreaseLightningDamage();
        });
    }

    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return false;
    }
}
