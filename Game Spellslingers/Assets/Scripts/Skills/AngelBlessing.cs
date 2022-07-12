using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelBlessing : Skill
{
    public AngelBlessing() : base(int.MaxValue) { }
    
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Player player = Player.instance;
            player.SetCurrentHealth(player.GetMaxHealth());
        });
    }

    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return false;
    }
}
