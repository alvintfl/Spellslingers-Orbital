using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulDrain : Skill
{
    public SoulDrain() : base(5) { }
    
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            Mage mage = (Mage)Player.instance;
            if (Level == 1)
            {
                mage.CastHealOnKill();
            }
            mage.IncreaseHealOnKill(2f);
            OnSelected(EventArgs.Empty);
        });
    }

    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return false;
    }
}
