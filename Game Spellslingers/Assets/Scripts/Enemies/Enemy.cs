using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Movement movement;
    [SerializeField] private Health health;

    private float _enemyDamage;

    public Enemy(float ed)
    {
        this._enemyDamage = ed;
    }
    public Movement Movement { get { return this.movement; } }
    public Health Health { get { return this.health; } }

    public float getEnemyDamage() {
        return this._enemyDamage;
    }
}
