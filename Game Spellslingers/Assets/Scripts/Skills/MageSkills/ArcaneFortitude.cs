using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 
 * <summary>
 * A skill that increases the player's
 * current health, max health and 
 * avoidance.
 * </summary>
 */
public class ArcaneFortitude : Skill
{
    public ArcaneFortitude() : base(5) { }
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Mage mage = (Mage) Player.instance;
            int increase = 15;
            mage.SetMaxHealth(mage.GetMaxHealth() + increase);
            mage.SetCurrentHealth(mage.GetCurrentHealth() + increase);
            float moveSpeed = mage.GetMoveSpeed();
            float newMoveSpeed = moveSpeed + moveSpeed * 0.05f;
            mage.SetMoveSpeed(newMoveSpeed);
        });
    }

    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return false;
    }
}
