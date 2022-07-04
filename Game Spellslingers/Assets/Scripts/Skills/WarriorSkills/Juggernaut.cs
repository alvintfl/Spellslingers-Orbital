using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juggernaut : Skill
{
    public Juggernaut() : base(5) { }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Warrior warrior = (Warrior)Player.instance;
            float moveSpeed = warrior.GetMoveSpeed();
            float newMoveSpeed = moveSpeed - moveSpeed * 0.015f;
            warrior.IncreaseArmour(2);
        });
    }

    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return false;
    }
}
