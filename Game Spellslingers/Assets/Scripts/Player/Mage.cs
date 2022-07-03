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

    public void IncreaseRange(float range)
    {
        this.cast.IncreaseRange(range);
    }

    public void CastLightningOrb()
    {
        this.cast.CastLightningOrb();
    }

    private void OnCastChange()
    {
        CastChange?.Invoke(this, EventArgs.Empty);
    }
}
