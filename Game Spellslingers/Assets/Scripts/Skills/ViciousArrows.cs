using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * <summary>
 * A class that increases the 
 * damage of the player's arrows.
 * </summary>
 */
public class ViciousArrows : Skill
{
    private Shoot shoot;
    public ViciousArrows() : base(10) { }
    public override void Start()
    {
        base.Start();
        Arrow.ResetDamage();
        this.shoot = Player.instance.gameObject.GetComponent<Shoot>();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            this.shoot.IncreaseDamage(10);
        });
    }
    public override void Reset()
    {
        Arrow.ResetDamage();
    }
}
