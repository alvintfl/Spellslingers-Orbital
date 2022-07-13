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
    private bool restoreOnAvoid;

    private int restoreAmount;

    /**
     * <summary>
     * An int to store chance to avoid
     * </summary>
    */
    private int avoidChance = 0;
    public delegate void AvoidChangeEventHandler<T, U>(T sender, U eventArgs);
    public event AvoidChangeEventHandler<Avoidance, EventArgs> AvoidChanceChange;

    private void Awake()
    {
        this.restoreOnAvoid = false;
        this.restoreAmount = 10;
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

    public bool AvoidRoll()
    {
        int roll = (int)UnityEngine.Random.Range(1f, 100f);
        if (roll <= avoidChance)
        {
            return true;
        }
        return false;
    }

    public bool GetRestoreOnAvoid()
    {
        return restoreOnAvoid;
    }
    
    public int GetRestoreAmount()
    {
        return this.restoreAmount;
    }

    public void SetRestoreOnAvoid(bool resBool)
    {
        restoreOnAvoid = resBool;
    }

    public void OnAvoidChanceChange()
    {
        AvoidChanceChange?.Invoke(this, EventArgs.Empty);
    }
}
