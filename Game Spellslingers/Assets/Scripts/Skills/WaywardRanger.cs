using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * <summary>
 * A signature skill that increases the 
 * number of projectiles, while making all projectiles fire
 * in random directions. 
 * </summary>
 */

public class WaywardRanger : Skill
{
    
    private PlayerShoot shoot;
    public WaywardRanger() : base(1) { }
    public override void Start()
    {
        base.Start();
        this.shoot = Player.instance.gameObject.GetComponent<PlayerShoot>();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            this.shoot.AddProjectiles(5);
            this.shoot.ToggleRandomiseProjectiles();
        });
    }
    public override void Reset() { }

    public override bool IsSignatureSkill()
    {
        return true;
    }
}
