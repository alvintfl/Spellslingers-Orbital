using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preparation : Skill
{
    public Preparation() : base(5) { }
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Arrow.ActivateLifeSteal();
            Lifesteal.IncreaseHealAmount();
        });
    }
}
