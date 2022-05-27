using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Player
{
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
}
