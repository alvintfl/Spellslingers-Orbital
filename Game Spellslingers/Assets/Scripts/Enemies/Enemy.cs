using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
    private EnemyMovement movement;
    private EnemyHealth health;

    private float _enemyDamage;

    public Enemy(float ed)
    {
        this._enemyDamage = ed;
    }

    private void Awake()
    {
        this.movement = gameObject.GetComponent<EnemyMovement>();
        this.health = gameObject.GetComponent<EnemyHealth>();    
    }
    public Movement Movement { get { return this.movement; } }
    public EnemyHealth Health { get { return this.health; } }

    public float getEnemyDamage() {
        return this._enemyDamage;
    }
}
