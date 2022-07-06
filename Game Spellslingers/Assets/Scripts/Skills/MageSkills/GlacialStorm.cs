using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlacialStorm : Skill
{
    [SerializeField] private GameObject auraOfFrost;
    public GlacialStorm() : base(1) { }
    
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Instantiate(this.auraOfFrost);
        });
    }

    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return true;
    }
}
