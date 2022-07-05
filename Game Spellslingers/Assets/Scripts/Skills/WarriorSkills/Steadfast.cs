using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steadfast : Skill
{
    public Steadfast() : base(1) { }
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Warrior warrior = (Warrior)Player.instance;
            warrior.IncreaseArmour(100);
            warrior.IncreaseRegen(10);
            float moveSpeed = warrior.GetMoveSpeed();
            float newMoveSpeed = moveSpeed - moveSpeed * 0.2f;

        });
    }
    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return true;
    }
}
