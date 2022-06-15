using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBloodMother : Enemy
{
    [SerializeField]
    private GameObject bloodChildrenPrefab;

    private float offsetX;
    private float offsetY;
    private Vector2 spawnPosition;

    public delegate void SpawnSpiders(GameObject spiders);
    public static event SpawnSpiders SpawnSpidersInfo;

    public override void Start()
    {
        base.Start();
        Death += SummonSpiders;
    }
    // on death effect
    private void SummonSpiders(Character sender, EventArgs e)
    {
        for (int i = 0; i < 5; i++)
        {
            spawnPosition = new Vector2(transform.position.x + UnityEngine.Random.Range(-1, 1),
                transform.position.y + UnityEngine.Random.Range(-1, 1));
            GameObject spawnedSpiders = Instantiate(bloodChildrenPrefab, spawnPosition, Quaternion.identity);
            SpawnSpidersInfo(spawnedSpiders);
        }
    }
}
