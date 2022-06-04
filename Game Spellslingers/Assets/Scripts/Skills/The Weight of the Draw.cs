using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWeightoftheDraw : Skill
{
    /** 
    * <summary>
    * A signature skill that massively increases damage in exchange for speed.
    * </summary>
    */
    private Shoot shoot;

    public TheWeightoftheDraw() : base(1) { }
    public override void Start()
    {
        base.Start();
        this.shoot = Player.instance.gameObject.GetComponent<Shoot>();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            this.shoot.IncreaseRate(-1f);
            this.shoot.SetDamageMulti(3);
            Arrow.ActivateStun();
        });
    }
    public override void Reset()
    {
        this.shoot.SetDamageMulti(1);
        Arrow.DeactivateStun();
    }
}

