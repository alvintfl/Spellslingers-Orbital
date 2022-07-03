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

    public delegate void SlamAreaInc(float increment);
    public static event SlamAreaInc slamAreaIncInfo;

    void OnEnable()
    {
        HammerSlamEvent.slamEventInfo += ExecuteAttack;
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
        slamAreaIncInfo(increment);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(slamPos.position, aoe);
    }

}
