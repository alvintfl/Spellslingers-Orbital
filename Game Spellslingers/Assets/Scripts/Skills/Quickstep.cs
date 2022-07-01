using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * <summary>
 * A skill that increases the 
 * player's movement speed 
 * and avoidance.
 * </summary>
 */
public class Quickstep : Skill
{
    public Quickstep() : base(5) { }
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Archer archer = (Archer) Player.instance;
            float moveSpeed = archer.GetMoveSpeed();
            float newMoveSpeed = moveSpeed + moveSpeed * 0.05f;
            archer.SetMoveSpeed(newMoveSpeed);

            archer.SetAvoidChance(archer.GetAvoidChance() + 5);
        }
         );
    }
    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return false;
    }
}
