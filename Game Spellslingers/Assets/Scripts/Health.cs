using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float currentHealth = 100f;
    public event EventHandler HealthChange;

    public Health(float currentHealth)
    {
        this.currentHealth = currentHealth;
    }

    public float CurrentHealth { 
        get { return currentHealth; } 
        set 
        { 
            this.currentHealth = value;
            OnHealthChange(EventArgs.Empty);
        } 
    }
    public virtual void TakeDamage(float damage)
    {
        Debug.Log("taking dmg");
        currentHealth -= damage;
        OnHealthChange(EventArgs.Empty);
    }
    protected virtual void OnHealthChange(EventArgs e)
    {
        HealthChange?.Invoke(this, e);
    }
}
