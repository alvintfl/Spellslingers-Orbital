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
             float moveSpeedIncrease = player.GetMoveSpeed() * 0.03f;
             player.SetMoveSpeed(moveSpeedIncrease);
         }
         );
    }
}
