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
    public static ExpManager instance;

    private bool isLeveling;
    private int maxExp;
    private int exp;
    private int level;
    private double multiplier;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        } else
        {
            instance = this;
        }
    }

    private void Start()
    {
        this.exp = 300;
        this.maxExp = 3;
        this.level = 1;
        this.multiplier = 1.2;
        this.isLeveling = false;
        ExpManager.LevelUp += IncreaseMaxExp;
        Enemy.EnemyDeath += AddExp;
        Player.instance.Death += StopExp;
        StartCoroutine(IsLevelUp());
        Skill.Selected += FinishLeveling;
    }

    private void OnDisable()
    {
        ExpManager.LevelUp -= IncreaseMaxExp;
        Enemy.EnemyDeath -= AddExp;
        Player.instance.Death -= StopExp;
    }

    private void FinishLeveling(Skill skill, EventArgs e)
    {
        this.isLeveling = false;
    }

    /** 
     * <summary>
     * Check if the player has enough exp to level up.
     * </summary>
     */
    private IEnumerator IsLevelUp()
    {
        while(true)
        {
            yield return new WaitUntil(() => !isLeveling);
            if (this.exp >= this.maxExp)
            {
                OnLevelUp(EventArgs.Empty);
            }
            yield return null;
        }
    }

    public int MaxExp { get { return this.maxExp; } }
    public int Exp { get { return this.exp; } }
    public int Level { get { return this.level; } }

    protected virtual void OnLevelUp(EventArgs e)
    {
        this.isLeveling = true;
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
