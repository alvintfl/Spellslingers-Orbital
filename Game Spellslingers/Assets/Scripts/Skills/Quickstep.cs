using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quickstep : Skill
{
    public Quickstep() : base(5) { }
    private void Start()
    {
        Button.onClick.AddListener(() =>
         {
             OnSelected(EventArgs.Empty);
             Player player = PlayerObject.GetComponent<Player>();
             float moveSpeedIncrease = player.GetMoveSpeed() * 0.03f;
             player.SetMoveSpeed(moveSpeedIncrease);
         });
    }
}
