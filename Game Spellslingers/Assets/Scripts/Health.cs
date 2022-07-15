using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that is responsible for health.
 * </summary>
 */
public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;
    public delegate void HealthChangeEventHandler<T, U>(T sender, U eventArgs);
    public event HealthChangeEventHandler<Health, EventArgs> HealthChange;
    public delegate void Died();
    public event Died DiedInfo;
    protected Animator anim;

    private void Awake()
    {
        this.anim = GetComponent<Animator>();
        this.currentHealth = maxHealth;
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            this.currentHealth = value;
            OnHealthChange();
        }
    }

    public float MaxHealth { get { return maxHealth; } set { this.maxHealth = value; } }
    public Animator Animator { get { return this.anim; } }
    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        OnHealthChange();
    }

    protected virtual void OnHealthChange()
    {
        HealthChange?.Invoke(this, EventArgs.Empty);
        CheckStatus();
    }

    private void DiesEvent()
    {
        // check function is subscribed
        if (DiedInfo != null)
            DiedInfo();
    }

    private void AnimateDeath()
    {
        if (this.anim != null)
        {
            this.anim.SetTrigger("Death");
        }
    }

    protected virtual void CheckStatus()
    {
        // health hits zero
        if (CurrentHealth <= 0)
        {
            AnimateDeath();
            this.DiesEvent();
        }
    }
}
