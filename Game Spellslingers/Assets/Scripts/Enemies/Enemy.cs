using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
    private Movement movement;
    private Health health;

    private float _enemyDamage;

    public Enemy(float ed)
    {
        this._enemyDamage = ed;
    }

    private void Awake()
    {
        this.movement = gameObject.GetComponent<Movement>();
        this.health = gameObject.GetComponent<Health>();    
    }
    public Movement Movement { get { return this.movement; } }
    public Health Health { get { return this.health; } }

    public float getEnemyDamage() {
        return this._enemyDamage;
    }
}
