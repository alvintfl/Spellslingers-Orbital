using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quickstep : Skill
{
    // Start is called before the first frame update
    void Start()
    {
        Button.onClick.AddListener(() =>
         {
             OnSelected(EventArgs.Empty);
             Player player = PlayerObject.GetComponent<Player>();
             float moveSpeedIncrease = player.GetMoveSpeed() * 0.1f;
             player.SetMoveSpeed(moveSpeedIncrease);
         });
    }
}
