using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A skill that lets the player heal
 * from dealing damage with their arrows.
 * </summary>
 */
public class BloodDrinker : Skill
{
    public BloodDrinker() : base(5) { }
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Lifesteal.IncreaseHeal(0.5f);
            Arrow.ActivateLifeSteal();
        });
    }

    public override void Reset()
    {
        Arrow.DeactivateLifeSteal();
        Lifesteal.Reset();
    }

    public override bool IsSignatureSkill()
    {
        return false;
    }
}
