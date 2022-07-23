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

    // apex form variables
    private bool apexFormEnabled;
    private bool inApex;
    private float armour;
    private float avoid;
    private float degenCooldown;
    private SpriteRenderer sr;

    public override void Awake()
    {
        base.Awake();
        this.cast = GetComponent<PlayerCast>();
        this.sr = GetComponent<SpriteRenderer>();
        this.damageTakenMultiplier = 1;
        this.apexFormEnabled = false;
    }

    public override void TakeDamage(float damage)
    {
        if (timeBtwAudio <= 0 && GetCurrentHealth() > 0)
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

        //Debug.Log(inApex);
        // activates Apex form
        if (apexFormEnabled && Input.GetKeyDown("space") && !inApex)
        {

            sr.color = new Color (251f/255f, 137f/255f, 70f/255f, 1f);
            this.inApex = true;
            // increase damage, cast speed
            IncreaseLightningDamage();
            IncreaseLightningDamage();
            IncreaseLightningDamage();
            IncreaseRate(0.5f);

            // Increase movespeed
            SetMoveSpeed(GetMoveSpeed() * 1.2f);

        }
        // deactivates Apex form
        else if (apexFormEnabled && Input.GetKeyDown("space") && inApex)
        {
            sr.color = Color.white;
            this.inApex = false;

            // decrease damage, cast speed
            DecreaseLightningDamage();
            DecreaseLightningDamage();
            DecreaseLightningDamage();
            IncreaseRate(-0.5f);

            // decrease movespeed
            SetMoveSpeed(GetMoveSpeed() / 1.2f);
        }

        // Apex form effects
        if (inApex) 
        {
            // degen
            if (degenCooldown <= 0f)
            {
                float increasedDamageTaken = 5 * damageTakenMultiplier;
                base.TakeDamage(increasedDamageTaken);
                degenCooldown = 1f;
            }
            else degenCooldown -= Time.deltaTime; 
        }
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

    public void DecreaseLightningDamage()
    {
        this.cast.DecreaseLightningDamage();
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

    public void ActivatePerfectStorm()
    {
        this.cast.ActivatePerfectStorm();
    }

    public void ActivateApexForm()
    {
        this.apexFormEnabled = true;
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

    public bool GetInApex()
    {
        return this.inApex;
    }

    public void SetInApex(bool apexFormState)
    {
        this.inApex = apexFormState;
    }
    public override string ToString()
    {
        return "Mage";
    }
}
