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
            Player player = Player.instance;
            int increase = 20;
            player.SetMaxHealth(player.GetMaxHealth() + increase);
            player.SetCurrentHealth(player.GetCurrentHealth() + increase);

            player.SetAvoidChance(player.GetAvoidChance() + 3);
        });
    }

    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return false;
    }
}
