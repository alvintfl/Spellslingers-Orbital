using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomber : Enemy
{
    [SerializeField] private GameObject explosionPrefab;
    public EnemyBomber() : base(3f, 1) { }

    public override void Die()
    {
        GameObject ex = Instantiate(explosionPrefab);
        ex.transform.position = gameObject.transform.position;  
        Destroy(gameObject);
    }
}
