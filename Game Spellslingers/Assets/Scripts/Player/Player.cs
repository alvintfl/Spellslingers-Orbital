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
    public string playerClass;
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
        // Snow Biome
        if (transform.position.x < -210f)
        {
            return 1;
        }
        // jungle biome
        if (transform.position.y < -247.8f)
        {
            return 2;
        }
        // lava Biome
        else if (transform.position.x > 155.9f)
        {
            return 3;
        }
        // ash biome
        else if (transform.position.y > 33.23f)
        {
            return 4;
        }
        // starting location
        else return 0;
    }
}
