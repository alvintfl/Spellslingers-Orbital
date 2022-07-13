using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Player
{
    private PlayerCast cast;
    [SerializeField] GameObject arcaneShieldPrefab;
    private GameObject arcaneShield;
    private WaitForSeconds arcaneShieldWait;
    private float damageTakenMultiplier;
    public event ChangeEventHandler<Mage, EventArgs> CastChange;

    // audio control
    private float timeBtwAudio;

    public override void Awake()
    {
        base.Awake();
        this.cast = GetComponent<PlayerCast>();
        this.damageTakenMultiplier = 1;
    }

    public override void TakeDamage(float damage)
    {
        if (timeBtwAudio <= 0)
        {
            AudioManager.instance.Play("mage_hit");
            timeBtwAudio = 1f;
        }

        if (this.arcaneShield != null)
        {
            if (this.arcaneShield.activeSelf)
            {
                this.arcaneShield.SetActive(false);
                StartCoroutine(RefreshArcaneShield());
                return;
            }
        }
        float increasedDamageTaken = damage * damageTakenMultiplier;
        base.TakeDamage(increasedDamageTaken);
    }

    void Update()
    {
        timeBtwAudio -= Time.deltaTime;
    }

    public void SetDamageDealtMultiplier(float multiplier)
    {
        this.cast.SetDamageDealtMultiplier(multiplier);
        OnCastChange();
    }

    public float GetDamageDealtMultiplier()
    {
        return this.cast.GetDamageDealtMultiplier();
    }

    public void SetDamageTakenMultiplier(float multiplier)
    {
        this.damageTakenMultiplier = multiplier;
        OnCastChange();
    }

    public float GetDamageTakenMultiplier()
    {
        return this.damageTakenMultiplier;
    }

    #region Lightning Methods
    public float GetLightningDamage()
    {
        return this.cast.GetLightningDamage();
    }

    public void IncreaseLightningDamage()
    {
        this.cast.IncreaseLightningDamage();
        OnCastChange();
    }

    public void UpgradeLightning()
    {
        this.cast.UpgradeLightning();
    }

    public void IncreaseRate(float secs)
    {
        this.cast.IncreaseRate(secs);
        OnCastChange();
    }

    public float GetRate()
    {
        return this.cast.GetRate();
    }

    public void IncreaseLightningRange()
    {
        this.cast.IncreaseLightningRange();
        OnCastChange();
    }

    public float GetLightningRange()
    {
        return this.cast.GetLightningRange();
    }
    #endregion

    #region LightningStorm Methods
    public void CastLightningStorm()
    {
        this.cast.CastLightningStorm();
        OnCastChange();
    }

    public void IncreaseLightningStormRange()
    {
        this.cast.IncreaseLightningStormRange();
        OnCastChange();
    }

    public void IncreaseLightningStormDuration()
    {
        this.cast.IncreaseLightningStormDuration();
        OnCastChange();
    }

    public void IncreaseLightningStormDamage()
    {
        this.cast.IncreaseLightningStormDamage();
        OnCastChange();
    }

    public float GetLightningStormDamage()
    {
        return this.cast.GetLightningStormDamage();
    }

    public float GetLightningStormRange()
    {
        return this.cast.GetLightningStormRange();
    }

    public float GetLightningStormDuration()
    {
        return this.cast.GetLightningStormDuration();
    }
    #endregion

    #region LightningOrb Methods
    public void CastLightningOrb()
    {
        this.cast.CastLightningOrb();
        OnCastChange();
    }

    public int GetLightningOrbCount()
    {
        return this.cast.GetLightningOrbCount();
    }

    public float GetLightningOrbDamage()
    {
        return this.cast.GetLightningOrbDamage();
    }

    public float GetLightningOrbRotationSpeed()
    {
        return this.cast.GetLightningOrbRotationSpeed();
    }

    public void IncreaseLightningOrbDamage()
    {
        this.cast.IncreaseLightningOrbDamage();
        OnCastChange();
    }
    #endregion

    public void CastHealOnKill()
    {
        this.cast.CastHealOnKill();
    }

    public void IncreaseHealOnKill(float heal)
    {
        this.cast.IncreaseHealOnKill(heal);
        OnCastChange();
    }

    public float GetHealOnKill()
    {
        return this.cast.GetHealOnKill();
    }

    public void CastArcaneShield()
    {
        this.arcaneShield = Instantiate(this.arcaneShieldPrefab);
        this.arcaneShieldWait = new WaitForSeconds(4f);
    }

    public IEnumerator RefreshArcaneShield()
    {
        yield return this.arcaneShieldWait;
        this.arcaneShield.SetActive(true);
    }


    private void OnCastChange()
    {
        CastChange?.Invoke(this, EventArgs.Empty);
    }

    public override string ToString()
    {
        return "Mage";
    }
}
