using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCast : MonoBehaviour
{
    [SerializeField] private GameObject lightningPrefab;
    private GameObject lightningObject;
    private Lightning lightning;

    [SerializeField] private GameObject lightningOrbPrefab;
    private LightningOrb lightningOrb;

    [SerializeField] private GameObject lightningBoltPrefab;
    private GameObject lightningBoltObject;
    private LightningBolt lightningBolt;

    [SerializeField] private GameObject healOnEnemyPrefab;
    private GameObject healOnEnemyObject;
    private HealOnEnemyKill healOnEnemyKill;

    private float rate;
    private WaitForSeconds lightningWait;
    private WaitForSeconds lightningBoltWait;
    private float damageDealtMultiplier;

    private void Awake()
    {
        this.lightningObject = Instantiate(this.lightningPrefab);
        this.lightning = lightningObject.GetComponent<Lightning>();

        this.lightningBoltObject = Instantiate(this.lightningBoltPrefab);
        this.lightningBolt = lightningBoltObject.GetComponent<LightningBolt>();

        this.healOnEnemyObject = Instantiate(this.healOnEnemyPrefab);
        this.healOnEnemyKill = healOnEnemyObject.GetComponent<HealOnEnemyKill>();

        this.rate = 1.8f;
        this.lightningWait = new WaitForSeconds(this.rate);
        this.lightningBoltWait = new WaitForSeconds(10f);
        this.damageDealtMultiplier = 1;
        StartCoroutine(CastLightning());
    }

    private void Start()
    {
        Player.instance.Death += StopCasting;
    }

    private void OnDestroy()
    {
        Player.instance.Death -= StopCasting;
    }

    private IEnumerator CastLightning()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            this.lightningObject.SetActive(true);
            yield return this.lightningWait;
        }
    }

    public void CastLightningOrb()
    {
        GameObject lightningOrbObject = Instantiate(this.lightningOrbPrefab);
        this.lightningOrb = lightningOrbObject.GetComponent<LightningOrb>();
    }

    public void CastLightningStorm()
    {
        StartCoroutine(CastLightningBolt());
    }

    private IEnumerator CastLightningBolt()
    {
        while (true)
        {
            this.lightningBoltObject.SetActive(true);
            yield return this.lightningBoltWait;
        }
    }


    public void IncreaseRate(float secs)
    {
        this.rate -= secs;
        this.lightningWait = new WaitForSeconds(this.rate);
    }

    public float GetRate()
    {
        return this.rate;
    }

    #region Lightning Methods
    public void IncreaseLightningDamage()
    {
        int damageIncrease = 10;
        float lightningDamageWithMultiplier = 
            (GetBaseLightningDamage() + damageIncrease) * this.damageDealtMultiplier;
        this.lightning.Damage = lightningDamageWithMultiplier;
    }

    public float GetLightningDamage()
    {
        return this.lightning.Damage;
    }

    private float GetBaseLightningDamage()
    {
        return this.lightning.GetBaseDamage() / this.damageDealtMultiplier;
    }
    
    public void IncreaseLightningRange()
    {
        this.lightning.IncreaseRange();
    }

    public float GetLightningRange()
    {
        return this.lightning.GetRange();
    }
    public void UpgradeLightning()
    {
        this.lightning.Upgrade();
    }
    #endregion

    #region LightningStorm Methods
    public void IncreaseLightningStormRange()
    {
        this.lightningBolt.IncreaseLightningFieldRange();
    }

    public void IncreaseLightningStormDuration()
    {
        this.lightningBolt.IncreaseLightningFieldDuration();
    }

    public void IncreaseLightningStormDamage()
    {
        int damageIncrease = 1;
        float lightningStormDamageWithMultiplier = 
            (GetBaseLightningStormDamage() + damageIncrease) * this.damageDealtMultiplier;
        this.lightningBolt.SetLightningFieldDamage(lightningStormDamageWithMultiplier);
    }

    public float GetLightningStormDamage()
    {
        return this.lightningBolt.GetLightningFieldDamage();
    }

    private float GetBaseLightningStormDamage()
    {
        return this.lightningBolt.GetLightningFieldDamage() / this.damageDealtMultiplier;
    }

    public float GetLightningStormRange()
    {
        return this.lightningBolt.GetLightningFieldRange();
    }

    public float GetLightningStormDuration()
    {
        return this.lightningBolt.GetLightningFieldDuration();
    }
    #endregion

    #region LightningOrb Methods
    public int GetLightningOrbCount()
    {
        return LightningOrb.Count;
    }
    public void IncreaseLightningOrbDamage()
    {
        int damageIncrease = 2;
        float lightningDamageWithMultiplier = 
            (GetBaseLightningOrbDamage() + damageIncrease) * this.damageDealtMultiplier;
        LightningOrb.Damage = lightningDamageWithMultiplier;
    }

    public float GetLightningOrbRotationSpeed()
    {
        return LightningOrb.RotationSpeed;
    }

    public float GetLightningOrbDamage()
    {
        return LightningOrb.Damage;
    }

    private float GetBaseLightningOrbDamage()
    {
        return LightningOrb.Damage / this.damageDealtMultiplier;
    }
    #endregion

    #region Heal On Kill Methods
    public void CastHealOnKill()
    {
        this.healOnEnemyObject.SetActive(true);
    }

    public void IncreaseHealOnKill(float heal)
    {
        this.healOnEnemyKill.IncreaseHeal(heal);
    }

    public float GetHealOnKill()
    {
        return this.healOnEnemyKill.HealAmount;
    }
    #endregion

    public void SetDamageDealtMultiplier(float multiplier)
    {
        // Get base values without multiplier
        float baseLightningDamage = GetBaseLightningDamage();
        float baseLightningOrbDamage = GetBaseLightningOrbDamage();
        float baseLightningStormDamage = this.lightningBolt != null ? GetBaseLightningStormDamage() : 0;

        this.damageDealtMultiplier = multiplier;

        float lightningDamageWithMultiplier = baseLightningDamage * this.damageDealtMultiplier;
        this.lightning.Damage = lightningDamageWithMultiplier;

        float lightningOrbDamageWithMultiplier = baseLightningOrbDamage * this.damageDealtMultiplier;
        LightningOrb.Damage = lightningOrbDamageWithMultiplier;

        if (this.lightningBolt != null)
        {
            float lightningStormDamageWithMultiplier = baseLightningStormDamage * this.damageDealtMultiplier;
            this.lightningBolt.SetLightningFieldDamage(lightningStormDamageWithMultiplier);
        }
    }
    public float GetDamageDealtMultiplier()
    {
        return this.damageDealtMultiplier;
    }

    private void StopCasting(Character sender, EventArgs e)
    {
        StopAllCoroutines();
    }
}
