using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private float _enemyDamage;
    private int exp;
    public static event EventHandler<DropExpEventArgs> DropExp;

    private void Start()
    {
        this.Health.DiedInfo += OnDropExp;
    }

    public Enemy(float ed, int exp)
    {
        this._enemyDamage = ed;
        this.exp = exp;
    }

    public float getEnemyDamage() {
        return this._enemyDamage;
    }

    public void OnDropExp()
    {
        DropExpEventArgs args = new DropExpEventArgs();
        args.Exp = this.exp;
        DropExp?.Invoke(this, args);
        Destroy(gameObject);
    }
}

public class DropExpEventArgs : EventArgs
{
    public int Exp { get; set; }
}
