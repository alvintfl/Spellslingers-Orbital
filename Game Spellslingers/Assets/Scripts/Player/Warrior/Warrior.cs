using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Player
{
    /**
     * <summary>
     * A class that represents Warrior. 
     * The warrior has a basic slam attack.
     * </summary> 
     */
    [SerializeField] private LayerMask enLayer;
    [SerializeField] private float aoe;
    [SerializeField] private Transform slamPos;
    [SerializeField] private GameObject slamGroundEffect;

    [SerializeField] private float attack;
    private float armour;
    private float regen;


    void OnEnable()
    {
        HammerSlamEvent.slamEventInfo += ExecuteAttack;
        regen = 0;
        InvokeRepeating("Regen", 0, 1.0f);
    }

    void OnDisable()
    {
        HammerSlamEvent.slamEventInfo -= ExecuteAttack;
    }
    /**
     * <summary>
     * A method that damages enemies in a circle.
     * </summary> 
     */


    private void ExecuteAttack()
    {
        Collider2D[] areaSlam = Physics2D.OverlapCircleAll(slamPos.position, aoe, enLayer);
        for (int i = 0; i < areaSlam.Length; i++)
        {
            areaSlam[i].GetComponent<Health>().TakeDamage(this.attack);
        }
        Instantiate(slamGroundEffect, slamPos.position, Quaternion.identity);
    }

    public void IncreaseSlamArea(float increment)
    {
        aoe += increment;
    }
    public float GetSlamArea()
    {
        return aoe;
    }

    public void IncreaseArmour(float increment)
    {
        armour += increment;
    }

    public float GetArmour()
    {
        return armour;
    }

    public void IncreaseRegen(float increment)
    {
        regen += increment;
    }

    public void SetAttack(float newAtk)
    {
        attack = newAtk;
    }

    public float GetAttack()
    {
        return attack;
    }


    /**
     * <summary>
     * Passively regenerates health
     * </summary>
     */

    private void Regen()
    {
        // regenerates
        if (health.CurrentHealth < health.MaxHealth)
        {
            health.TakeDamage(-regen);
            if (health.CurrentHealth > health.MaxHealth)
            {
                health.CurrentHealth = health.MaxHealth;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(slamPos.position, aoe);
    }

    /**
     * <summary>
     * Check the warrior's defences and deal damage accordingly.
     * </summary>
     */
    public override void TakeDamage(float damage)
    {
        // aim for this formula is to make armour more effective vs smaller hits
        // and less effective against larger hits
        base.TakeDamage(damage * damage / (armour + damage));
    }
}