using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostShots : Skill
{
    public FrostShots() : base(5) { }
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Arrow.ActivateFrostArrow();
            Slow.IncreaseSlow(0.05f);
            Slow.IncreaseDuration(1f);
        });
    }
}
