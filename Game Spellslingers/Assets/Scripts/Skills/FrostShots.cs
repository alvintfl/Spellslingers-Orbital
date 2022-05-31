using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostShots : Skill
{
    public FrostShots() : base(1) { }
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Arrow.ActivateFrostArrow();
        });
    }
    public override void Reset()
    {
        Arrow.DeactivateFrostArrow();
    }
}
