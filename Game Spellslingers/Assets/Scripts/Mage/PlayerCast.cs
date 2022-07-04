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

    private float rate;
    private WaitForSeconds lightningWait;
    private WaitForSeconds lightningBoltWait;
    private float damageDealtMultiplier;

    private void Start()
    {
        this.lightningObject = Instantiate(this.lightningPrefab);
        this.lightning = lightningObject.GetComponent<Lightning>();

        this.rate = 1.8f;
        this.lightningWait = new WaitForSeconds(this.rate);
        this.lightningBoltWait = new WaitForSeconds(10f);
        this.damageDealtMultiplier = 1;
        StartCoroutine(CastLightning());
    }

    private IEnumerator CastLightning()
    {
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
        this.lightningBoltObject = Instantiate(this.lightningBoltPrefab);
        this.lightningBolt = lightningBoltObject.GetComponent<LightningBolt>();
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

    public void IncreaseLightningDamage()
    {
        int damageIncrease = 10;
        float lightningDamageWithMultiplier = 
            (GetBaseLightningDamage() + damageIncrease) * this.damageDealtMultiplier;
        this.lightning.Damage = lightningDamageWithMultiplier;
    }

    private float GetBaseLightningDamage()
    {
        return this.lightning.Damage / this.damageDealtMultiplier;
    }
    
    public void IncreaseLightningRange()
    {
        this.lightning.IncreaseRange();
    }

    public void IncreaseLightningStormRange()
    {
        this.lightningBolt.IncreaseRange();
    }

    public void IncreaseLightningOrbDamage()
    {
        int damageIncrease = 2;
        float lightningDamageWithMultiplier = 
            (GetBaseLightningOrbDamage() + damageIncrease) * this.damageDealtMultiplier;
        LightningOrb.Damage = lightningDamageWithMultiplier;
    }

    private float GetBaseLightningOrbDamage()
    {
        return LightningOrb.Damage / this.damageDealtMultiplier;
    }

    public void SetDamageDealtMultiplier(float multiplier)
    {
        // Get base values without multiplier
        float baseLightningDamage = GetBaseLightningDamage();
        float baseLightningOrbDamage = GetBaseLightningOrbDamage();

        this.damageDealtMultiplier = multiplier;

        // Calculate new values with new multiplier
        float lightningDamageWithMultiplier = baseLightningDamage * this.damageDealtMultiplier;
        this.lightning.Damage = lightningDamageWithMultiplier;
        float lightningOrbDamageWithMultiplier = baseLightningOrbDamage * this.damageDealtMultiplier;
        LightningOrb.Damage = lightningOrbDamageWithMultiplier;
        
    }
    public float GetDamageDealtMultiplier()
    {
        return this.damageDealtMultiplier;
    }
}
