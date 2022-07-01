using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Player
{
    private PlayerCast cast;
    private float damageTakenMultiplier;
    private float damageDealtMultiplier;
    public event ChangeEventHandler<Mage, EventArgs> CastChange;

    public override void Awake()
    {
        base.Awake();
        this.cast = GetComponent<PlayerCast>();
        this.damageTakenMultiplier = 1;
        this.damageDealtMultiplier = 1;
    }

    public override void TakeDamage(float damage)
    {
        float increasedDamageTaken = damage * damageTakenMultiplier;
        base.TakeDamage(increasedDamageTaken);
    }

    public void SetDamageDealtMultiplier(float multiplier)
    {
        this.damageDealtMultiplier = multiplier;
        float lightningDamageWithMultiplier = GetLightningDamage() * this.damageDealtMultiplier;
        this.cast.SetLightningDamage(lightningDamageWithMultiplier);
    }

    public float GetDamageDealtMultiplier()
    {
        return this.damageDealtMultiplier;
    }

    public void SetDamageTakenMultiplier(float multiplier)
    {
        this.damageTakenMultiplier = multiplier;
    }

    public float GetDamageTakenMultiplier()
    {
        return this.damageTakenMultiplier;
    }

    public void SetLightningDamage(float damage)
    {
        this.cast.SetLightningDamage(damage);
        float lightningDamageWithMultiplier = GetLightningDamage() * this.damageDealtMultiplier;
        this.cast.SetLightningDamage(lightningDamageWithMultiplier);
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

    public void IncreaseRange(float range)
    {
        this.cast.IncreaseRange(range);
    }

    private void OnCastChange()
    {
        CastChange?.Invoke(this, EventArgs.Empty);
    }
}
