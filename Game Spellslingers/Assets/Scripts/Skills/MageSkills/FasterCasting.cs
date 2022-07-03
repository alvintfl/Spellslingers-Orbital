using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterCasting : Skill
{
    public FasterCasting() : base(10) { }
    
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Mage mage = (Mage) Player.instance;
            mage.IncreaseRate(0.1f);
        });
    }

    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return false;
    }
}
