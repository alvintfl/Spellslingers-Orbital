using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * <summary>
 * A class that increases the 
 * number of projectiles the 
 * player can shoot.
 * </summary>
 */
public class MoreShots : Skill
{
    private PlayerShoot shoot;
    public MoreShots() : base(10) { }
    public override void Start()
    {
        base.Start();
        this.shoot = Player.instance.gameObject.GetComponent<PlayerShoot>();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            this.shoot.AddProjectiles(1);
        });
    }
    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return false;
    }
}
