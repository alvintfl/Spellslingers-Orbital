using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Player
{
    PlayerShoot shoot;

    private void Start()
    {
        this.shoot = GetComponent<PlayerShoot>();
    }

    public PlayerShoot Shoot { get { return this.shoot; } }
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
