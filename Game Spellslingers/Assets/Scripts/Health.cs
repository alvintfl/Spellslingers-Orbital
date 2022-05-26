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
    private float currentHealth = 100f;
    public event EventHandler HealthChange;
    public delegate void Died();
    public event Died DiedInfo;
    private Animator anim;

    private void Awake() {
        this.anim = GetComponent<Animator>();
    }

    private void Start()
    {
        this.currentHealth = maxHealth;
    }

    public float CurrentHealth { get { return currentHealth; } 
        set 
        { 
            this.currentHealth = value;
            OnHealthChange(EventArgs.Empty);
        } 
    }

    public float MaxHealth { get { return maxHealth; }  set { this.maxHealth = value;  } }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        OnHealthChange(EventArgs.Empty);
    }
    protected virtual void OnHealthChange(EventArgs e)
    {
        HealthChange?.Invoke(this, e);
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
