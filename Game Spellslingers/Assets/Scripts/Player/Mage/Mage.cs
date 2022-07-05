using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Player
{
    private PlayerCast cast;
    private float damageTakenMultiplier;
    public event ChangeEventHandler<Mage, EventArgs> CastChange;

    public override void Awake()
    {
        base.Awake();
        this.cast = GetComponent<PlayerCast>();
        this.damageTakenMultiplier = 1;
    }

    public override void TakeDamage(float damage)
    {
        float increasedDamageTaken = damage * damageTakenMultiplier;
        base.TakeDamage(increasedDamageTaken);
    }

    public void SetDamageDealtMultiplier(float multiplier)
    {
        this.cast.SetDamageDealtMultiplier(multiplier);
    }

    public float GetDamageDealtMultiplier()
    {
        return this.cast.GetDamageDealtMultiplier();
    }

    public void SetDamageTakenMultiplier(float multiplier)
    {
        this.damageTakenMultiplier = multiplier;
    }

    public float GetDamageTakenMultiplier()
    {
        return this.damageTakenMultiplier;
    }

    public void IncreaseLightningDamage()
    {
        this.cast.IncreaseLightningDamage();
    }

    public void IncreaseLightningOrbDamage()
    {
        this.cast.IncreaseLightningOrbDamage();
    }

    public void IncreaseRate(float secs)
    {
        this.cast.IncreaseRate(secs);
        OnCastChange();
    }

    public void IncreaseLightningRange()
    {
        this.cast.IncreaseLightningRange();
    }

    public void IncreaseLightningStormRange()
    {
        this.cast.IncreaseLightningStormRange();
    }

    public void IncreaseLightningStormDuration()
    {
        this.cast.IncreaseLightningStormDuration();
    }

    public void IncreaseLightningStormDamage()
    {
        this.cast.IncreaseLightningStormDamage();
    }

    public void CastLightningOrb()
    {
        this.cast.CastLightningOrb();
    }

    public void CastLightningStorm()
    {
        this.cast.CastLightningStorm();
    }

    public void UpgradeLightning()
    {
        this.cast.UpgradeLightning();
    }

    private void OnCastChange()
    {
        CastChange?.Invoke(this, EventArgs.Empty);
    }
}
