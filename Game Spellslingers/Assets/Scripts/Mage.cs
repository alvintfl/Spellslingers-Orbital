using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Player
{
    private PlayerCast cast;
    public event ChangeEventHandler<Mage, EventArgs> CastChange;

    public override void Awake()
    {
        base.Awake();
        this.cast = GetComponent<PlayerCast>();
    }

    public void SetLightningDamage(float damage)
    {
        this.cast.SetLightningDamage(damage);
    }

    public float GetLightningDamage()
    {
        return this.cast.GetLightningDamage();
    }

    public void IncreaseRate(float secs)
    {
        this.cast.IncreaseRate(secs);
        OnCastChange();
    }

    private void OnCastChange()
    {
        CastChange?.Invoke(this, EventArgs.Empty);
    }
}
