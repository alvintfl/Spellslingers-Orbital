using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreShots : Skill
{
    private Shoot shoot;
    public MoreShots() : base(10) { }
    public override void Start()
    {
        base.Start();
        this.shoot = Player.instance.gameObject.GetComponent<Shoot>();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            this.shoot.AddProjectiles();
        });
    }
}
