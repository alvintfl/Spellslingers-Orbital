using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpManager : MonoBehaviour
{
    public static event EventHandler LevelUp;
    public event EventHandler GainMaxExp;
    public event EventHandler GainExp;

    private int maxExp;
    private int exp;

    private void Start()
    {
        this.exp = 0;
        this.maxExp = 5;
        ExpManager.LevelUp += IncreaseMaxExp;
        Enemy.DropExp += AddExp;
    }

    private void Update()
    {
        IsLevelUp();
    }

    private void OnDisable()
    {
        ExpManager.LevelUp -= IncreaseMaxExp;
        Enemy.DropExp -= AddExp;
    }

    private void IsLevelUp()
    {
        if (this.exp >= this.maxExp)
        {
            OnLevelUp(EventArgs.Empty);
        }
    }

    public int MaxExp { get { return this.maxExp; } }
    public int Exp { get { return this.exp; } }

    protected virtual void OnLevelUp(EventArgs e)
    {
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

    public void IncreaseMaxExp(object sender, EventArgs e)
    {
        this.exp -= this.maxExp;
        this.maxExp = (int) (this.maxExp * 1.2);
        OnGainMaxExp(EventArgs.Empty);
    }

    public void AddExp(object sender, DropExpEventArgs e)
    {
        this.exp += e.Exp;
        Debug.Log(this.maxExp);
        OnGainExp(EventArgs.Empty); 
    }
}
