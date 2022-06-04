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
    public static Player instance;
    public string playerClass;

    private int level;

    public int Level { get { return this.level; } set { this.level = value; } }

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
}
