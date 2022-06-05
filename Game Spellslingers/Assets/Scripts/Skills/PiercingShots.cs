using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * <summary>
 * A class that allows the player's
 * arrows to pierce enemies.
 * </summary>
 */
public class PiercingShots : Skill
{
    public PiercingShots() : base(5) { }

    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Arrow.setPierceMax(Arrow.getPierceMax() + 1);
        });
    }
    public override void Reset()
    {
        Arrow.ResetPierceMax();
    }

    public override bool IsSignatureSkill()
    {
        return false;
    }

}
