using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A skill that allows arrows to slow.
 * </summary>
 */
public class FrostShots : Skill
{
    [SerializeField] private Sprite arrow;
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

    public override bool IsSignatureSkill()
    {
        return true;
    }
}
