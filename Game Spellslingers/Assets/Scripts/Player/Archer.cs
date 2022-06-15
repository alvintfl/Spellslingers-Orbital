using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Player
{
    PlayerShoot shoot;
    public event ChangeEventHandler<Archer, EventArgs> ShootChange;

    public override void Awake()
    {
        base.Awake();
        this.shoot = GetComponent<PlayerShoot>();
        this.shoot.PlayerShootChange += OnArcherShootChange;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        this.shoot.PlayerShootChange -= OnArcherShootChange;
    }

    public int GetProjectileCount()
    {
        return this.shoot.GetProjectileCount();
    }

    public float GetRate()
    {
        return this.shoot.GetRate();
    }

    private void OnArcherShootChange(PlayerShoot sender, EventArgs e)
    {
        ShootChange?.Invoke(this, e);
    }

    //public PlayerShoot Shoot { get { return this.shoot; } }
    /*
    private int projectiles;
    private int pierces;

    public int Projectiles 
    { 
        get
        {
            return projectiles;
        }
        set 
        {
            projectiles = value;
        }
    }

    public int Pierces
    {
        get
        {
            return pierces;
        }
        set
        {
            pierces = value;
        }
    }
    */
}
