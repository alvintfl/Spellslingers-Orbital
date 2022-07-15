using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that represents the player.
 * </summary>
 */
public class Player : Character
{
    public static Player instance { get; private set; }

    public override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public int FindCurrentLocation()
    {
        // Snowfields
        if (transform.position.x < -215f)
        {
            return 1;
        }
        // jungle
        if (transform.position.y < -247.8f)
        {
            return 2;
        }
        // Volcano
        else if (transform.position.x > 155.9f)
        {
            return 3;
        }
        // ash basin
        else if (transform.position.y > 38.23f)
        {
            return 4;
        }
        // dry sea
        else
        {
            return 0;
        }
    }

    public override string ToString()
    {
        return "Player";
    }
}
