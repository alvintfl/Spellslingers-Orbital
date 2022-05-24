using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingShots : Skill
{
    public PiercingShots() : base(5) { }

    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Arrow.setPierceMax(Arrow.getPierceMax() + 1);
        });
    }

}
