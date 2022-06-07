using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that is responsible for avoidance.
 * </summary>
 */
public class Avoidance : MonoBehaviour
{
    /**
     * <summary>
     * A bool to check if skills give restore life
     * on avoiding attack.
     * </summary>
    */
    private bool restoreOnAvoid = false;


    /**
     * <summary>
     * An int to store chance to avoid
     * </summary>
    */
    private int avoidChance = 0;
    public event EventHandler<AvoidanceArgs> AvoidChanceChange;


    public Avoidance(int chance)
    {
        this.avoidChance = chance;
    }

    public int GetAvoidChance() 
    {
        return avoidChance;
    }
    public void SetAvoidChance(int value) 
    {
        avoidChance = value;
        OnAvoidChanceChange();
    }

    public bool avoidRoll()
    {
        int roll = (int) UnityEngine.Random.Range(1f, 100f);
        if (roll <= avoidChance)
        {
            print("damage avoided");
            return true;
        }
        return false;
    }

    public bool GetRestoreOnAvoid() 
    {
        return restoreOnAvoid;
    }

    public void SetRestoreOnAvoid(bool resBool)
    {
        restoreOnAvoid = resBool;
    }

    public void OnAvoidChanceChange()
    {
        AvoidanceArgs args = new AvoidanceArgs();
        args.AvoidChance = this.avoidChance;
        AvoidChanceChange?.Invoke(this, args);
    }
}

public class AvoidanceArgs : EventArgs
{
    public int AvoidChance { get; set; }
}
