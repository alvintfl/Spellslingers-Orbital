using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDrinker : Skill
{
    public BloodDrinker() : base(5) { }
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Arrow.ActivateLifeSteal();
            Lifesteal.IncreaseHeal(0.05f);
        });
    }

    public override void Reset()
    {
        Arrow.DeactivateLifeSteal();
        Lifesteal.Reset();
    }
}
