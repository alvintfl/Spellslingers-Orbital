using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frenzy : Skill
{
    public Frenzy() : base(1) { }
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Warrior warrior = (Warrior)Player.instance;
            float moveSpeed = warrior.GetMoveSpeed();
            float newMoveSpeed = moveSpeed + moveSpeed * 0.5f;
            warrior.ActivateFrenzy();
        });
    }
    public override void Reset() 
    {
        Warrior warrior = (Warrior)Player.instance;
        warrior.DeactivateFrenzy();
    }

    public override bool IsSignatureSkill()
    {
        return true;
    }
}
