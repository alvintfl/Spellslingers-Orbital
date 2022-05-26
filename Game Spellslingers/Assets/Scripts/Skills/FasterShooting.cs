using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * <summary>
 * A class that increases the player's
 * shooting rate.
 * </summary>
 */
public class FasterShooting : Skill
{
    private Shoot shoot;
    public FasterShooting() : base(10) { }
    public override void Start()
    {
        base.Start();
        this.shoot = Player.instance.gameObject.GetComponent<Shoot>();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            this.shoot.IncreaseRate(0.05f);
        });
    }

}
