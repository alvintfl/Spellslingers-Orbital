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
             Movement player = Player.instance.Movement;
             float moveSpeed = player.GetMoveSpeed();
             float newMoveSpeed = moveSpeed + moveSpeed * 0.05f;
             player.SetMoveSpeed(newMoveSpeed);

             Avoidance playerAvoidance = Player.instance.Avoidance;
             playerAvoidance.SetAvoidChance(playerAvoidance.GetAvoidChance() + 5);
         }
         );
    }
    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return false;
    }
}
