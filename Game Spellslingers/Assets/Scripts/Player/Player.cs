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

    private Avoidance avoid;
    public event ChangeEventHandler<Player, EventArgs> AvoidChanceChange;

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
        this.avoid = gameObject.GetComponent<Avoidance>();
        this.avoid.AvoidChanceChange += OnPlayerAvoidChanceChange;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        this.avoid.AvoidChanceChange -= OnPlayerAvoidChanceChange;
    }

    #region Avoidance Methods
    public int GetAvoidChance()
    {
        return this.avoid.GetAvoidChance();
    }

    public void SetAvoidChance(int value)
    {
        this.avoid.SetAvoidChance(value);
    }

    public bool AvoidRoll()
    {
        return this.avoid.AvoidRoll();
    }

    public bool GetRestoreOnAvoid()
    {
        return this.avoid.GetRestoreOnAvoid();
    }

    public void SetRestoreOnAvoid(bool resBool)
    {
        this.avoid.SetRestoreOnAvoid(resBool);
    }

    private void OnPlayerAvoidChanceChange(Avoidance sender, EventArgs e)
    {
        AvoidChanceChange?.Invoke(this, EventArgs.Empty);
    }
    #endregion
}
