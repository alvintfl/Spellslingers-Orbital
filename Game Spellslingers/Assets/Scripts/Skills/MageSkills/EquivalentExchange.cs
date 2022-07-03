using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquivalentExchange : Skill
{
    public EquivalentExchange() : base(5) { }
    
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Mage mage = (Mage) Player.instance;
            mage.SetDamageTakenMultiplier(mage.GetDamageTakenMultiplier() * 1.1f);
            mage.SetDamageDealtMultiplier(mage.GetDamageDealtMultiplier() * 1.1f);
        });
    }

    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return false;
    }
}
