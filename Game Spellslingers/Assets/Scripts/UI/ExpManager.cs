using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 
 * <summary>
 * A class that manages 
 * the player's exp.
 * </summary>
 */
public class ExpManager : MonoBehaviour
{
    public delegate void ExpEventHandler<T, U>(T sender, U eventArgs);
    public static event ExpEventHandler<ExpManager, EventArgs> LevelUp;
    public event ExpEventHandler<ExpManager, EventArgs> GainMaxExp;
    public event ExpEventHandler<ExpManager, EventArgs> GainExp;

    private int maxExp;
    private int exp;
    private int level;
    private double multiplier;

    private void Start()
    {
        this.exp = 0;
        this.maxExp = 3;
        this.level = 1;
        this.multiplier = 1.2;
        ExpManager.LevelUp += IncreaseMaxExp;
        Enemy.DropExp += AddExp;
        Player.instance.Death += StopExp;
    }

    private void Update()
    {
        IsLevelUp();
    }

    private void OnDisable()
    {
        ExpManager.LevelUp -= IncreaseMaxExp;
        Enemy.DropExp -= AddExp;
        Player.instance.Death -= StopExp;
    }

    /** 
     * <summary>
     * Check if the player has enough exp to level up.
     * </summary>
     */
    private void IsLevelUp()
    {
        if (this.exp >= this.maxExp)
        {
            OnLevelUp(EventArgs.Empty);
        }
    }

    public int MaxExp { get { return this.maxExp; } }
    public int Exp { get { return this.exp; } }
    public int Level { get { return this.level; } }

    protected virtual void OnLevelUp(EventArgs e)
    {
        this.level++;
        LevelUp?.Invoke(this, e);
    }

    protected virtual void OnGainMaxExp(EventArgs e)
    {
        GainMaxExp?.Invoke(this, e);
    }

    protected virtual void OnGainExp(EventArgs e)
    {
        GainExp?.Invoke(this, e);
    }

    public void IncreaseMaxExp(ExpManager sender, EventArgs e)
    {
        this.exp -= this.maxExp;
        this.maxExp = (int)Math.Ceiling(this.MaxExp * this.multiplier);
        OnGainMaxExp(EventArgs.Empty);
    }

    public void AddExp(Enemy sender, EventArgs e)
    {
        this.exp += sender.Exp;
        OnGainExp(EventArgs.Empty);
    }

    private void StopExp(Character sender, EventArgs e)
    {
        OnDisable();
    }
}
