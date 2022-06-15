using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindDancer : Skill
{
    public WindDancer() : base(1) { }
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Player player = Player.instance;
            player.SetAvoidChance(player.GetAvoidChance() + 30);
            player.SetRestoreOnAvoid(true);
        });
    }
    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return true;
    }
}
