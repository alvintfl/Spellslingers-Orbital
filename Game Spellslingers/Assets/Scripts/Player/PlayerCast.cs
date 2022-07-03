using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCast : MonoBehaviour
{
    private GameObject lightningObject;
    private Lightning lightning;
    [SerializeField] private GameObject lightningOrbPrefab;
    private LightningOrb lightningOrb;
    private float rate;
    private WaitForSeconds wait;
    private float directionMagnitude;
    private float damageDealtMultiplier;

    private void Start()
    {
        GameObject lightningPrefab = transform.GetChild(0).gameObject;
        this.lightningObject = Instantiate(lightningPrefab);
        this.lightningObject.transform.SetParent(gameObject.transform);
        this.lightningObject.SetActive(false);
        this.lightning = lightningObject.GetComponent<Lightning>();

        this.rate = 1.8f;
        this.wait = new WaitForSeconds(this.rate);
        this.directionMagnitude = 3.5f;
        this.damageDealtMultiplier = 1;
        StartCoroutine(CastLightning());
    }

    private IEnumerator CastLightning()
    {
        while (true)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 playerPosition = gameObject.transform.position;
            Vector2 mouseDirection = mousePosition - playerPosition;
            mouseDirection.Normalize();
            float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg - 90;
            this.lightningObject.transform.rotation = Quaternion.Euler(0, 0, angle);
            this.lightningObject.transform.position = playerPosition + mouseDirection * this.directionMagnitude;
            this.lightningObject.SetActive(true);
            yield return this.wait;
        }
    }

    public void CastLightningOrb()
    {
        GameObject lightningOrbObject = Instantiate(this.lightningOrbPrefab);
        this.lightningOrb = lightningOrbObject.GetComponent<LightningOrb>();
    }

    public void IncreaseRate(float secs)
    {
        this.rate -= secs;
        this.wait = new WaitForSeconds(this.rate);
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
    
    public void IncreaseRange(float multiplier)
    {
        this.lightningObject.transform.localScale *= multiplier;
        this.directionMagnitude += 0.25f;
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
