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
public class Entrench : Skill
{
    public Entrench() : base(5) { }
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Archer archer = (Archer) Player.instance;
            int increase = 20;
            archer.SetMaxHealth(archer.GetMaxHealth() + increase);
            archer.SetCurrentHealth(archer.GetCurrentHealth() + increase);

            archer.SetAvoidChance(archer.GetAvoidChance() + 3);
        });
    }

    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return false;
    }
}
