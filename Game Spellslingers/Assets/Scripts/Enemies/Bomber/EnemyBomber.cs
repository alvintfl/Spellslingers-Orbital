using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that represents the bomber enemy.
 * </summary>
 */
public class EnemyBomber : Enemy
{
    [SerializeField] private GameObject explosionPrefab;

    private void Update()
    {
        Debug.Log(GetCurrentHealth());
    }

    /**
     * <summary>
     * Create an explosion when the bomber dies.
     * </summary>
     */
    public override void Die()
    {
        GameObject ex = Instantiate(explosionPrefab);
        ex.transform.position = gameObject.transform.position;  
        Destroy(gameObject);
    }
}
