using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Player
{
    private PlayerShoot shoot;
    private Avoidance avoid;
    public event ChangeEventHandler<Archer, EventArgs> ShootChange;
    public event ChangeEventHandler<Archer, EventArgs> AvoidChanceChange;

    private bool unwaveringEnabled;

    // audio control
    private float timeBtwAudio;

    public override void Awake()
    {
        base.Awake();
        this.shoot = GetComponent<PlayerShoot>();
        this.shoot.PlayerShootChange += OnArcherShootChange;
        this.avoid = gameObject.GetComponent<Avoidance>();
        this.avoid.AvoidChanceChange += OnPlayerAvoidChanceChange;

        unwaveringEnabled = false;
    }

    void Update()
    {
        timeBtwAudio -= Time.deltaTime;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        this.shoot.PlayerShootChange -= OnArcherShootChange;
        this.avoid.AvoidChanceChange -= OnPlayerAvoidChanceChange;
    }

    /**
     * <summary>
     * Check the archer's avoid chance and deal damage accordingly.
     * </summary>
     */
    public override void TakeDamage(float damage)
    {
        if (!unwaveringEnabled)
        {
            if (!this.avoid.AvoidRoll())
            {
                if (timeBtwAudio <= 0)
                {
                    AudioManager.instance.Play("archer_hit");
                    timeBtwAudio = 1f;
                }
                base.TakeDamage(damage);
            }
            else if (GetRestoreOnAvoid())
            {
                SetCurrentHealth(GetCurrentHealth() + this.avoid.GetRestoreAmount());
            }
        }
        else 
        {
            base.TakeDamage(damage * damage / (avoid.GetAvoidChance() + damage));
        }
    }

    public int GetProjectileCount()
    {
        return this.shoot.GetProjectileCount();
    }

    public float GetRate()
    {
        return this.shoot.GetRate();
    }

    private void OnArcherShootChange(PlayerShoot sender, EventArgs e)
    {
        ShootChange?.Invoke(this, e);
    }

    public void ActivateUnwaveringInstinct()
    {
        unwaveringEnabled = true;
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
