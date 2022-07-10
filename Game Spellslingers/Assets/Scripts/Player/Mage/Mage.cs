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

    public void UpgradeLightning()
    {
        this.cast.UpgradeLightning();
    }

    private void OnCastChange()
    {
        CastChange?.Invoke(this, EventArgs.Empty);
    }
}
