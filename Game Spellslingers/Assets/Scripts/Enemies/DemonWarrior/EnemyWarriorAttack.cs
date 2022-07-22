using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWarriorAttack : MonoBehaviour
{
    [SerializeField]
    private float attack;
    [SerializeField]
    private GameObject slamGroundEffect;
    [SerializeField] 
    private Transform slamPos;
    [SerializeField] 
    private LayerMask plLayer;
    [SerializeField] 
    private float aoe;

    private void ExecuteAttack()
    {
        Collider2D[] areaSlam = Physics2D.OverlapCircleAll(slamPos.position, aoe, plLayer);
        for (int i = 0; i < areaSlam.Length; i++)
        {
            areaSlam[i].GetComponent<Health>().TakeDamage(attack);
        }
        Instantiate(slamGroundEffect, slamPos.position, Quaternion.identity);
    }
}
