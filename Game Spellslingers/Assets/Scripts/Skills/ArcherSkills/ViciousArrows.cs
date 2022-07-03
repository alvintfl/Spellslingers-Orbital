using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * <summary>
 * A skill that increases the 
 * damage of the player's arrows.
 * </summary>
 */
public class ViciousArrows : Skill
{
    public ViciousArrows() : base(10) { }
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Arrow.IncreaseDamage(10);
        });
    }
    public override void Reset()
    {
        Arrow.ResetDamage();
    }

    public override bool IsSignatureSkill()
    {
        return false;
    }
}
