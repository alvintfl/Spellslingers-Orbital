using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/** 
* <summary>
* A signature skill that massively increases damage in exchange for speed.
* </summary>
*/

public class TheWeightoftheDraw : Skill
{

    private PlayerShoot shoot;

    public TheWeightoftheDraw() : base(1) { }
    public override void Start()
    {
        base.Start();
        this.shoot = Player.instance.gameObject.GetComponent<PlayerShoot>();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            this.shoot.DecreaseRate(2f);
            Arrow.SetDamageMulti(3);
            Arrow.ActivateStun();
        });
    }
    public override void Reset()
    {
        Arrow.SetDamageMulti(1);
        Arrow.DeactivateStun();
    }
    
    public override bool IsSignatureSkill()
    {
        return true;
    }
}

