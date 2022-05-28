using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * <summary>
 * A class that increases the 
 * player's movement speed.
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
             float newMoveSpeed = moveSpeed + moveSpeed * 0.03f;
             player.SetMoveSpeed(newMoveSpeed);

             Avoidance playerAvoidance = Player.instance.Avoidance;
             playerAvoidance.setAvoidChance(playerAvoidance.getAvoidChance() + 5);
         }
         );
    }
}
