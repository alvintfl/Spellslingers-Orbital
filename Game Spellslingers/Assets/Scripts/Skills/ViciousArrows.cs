using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViciousArrows : Skill
{
    private Shoot shoot;
    public ViciousArrows() : base(10) { }
    public override void Start()
    {
        base.Start();
        this.shoot = Player.instance.gameObject.GetComponent<Shoot>();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            this.shoot.IncreaseDamage(10);
        });
    }
}
