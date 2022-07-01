using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overreach : Skill
{
    public Overreach() : base(5) { }
    
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Mage mage = (Mage) Player.instance;
            mage.IncreaseRange(1.1f);
        });
    }

    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return false;
    }
}
