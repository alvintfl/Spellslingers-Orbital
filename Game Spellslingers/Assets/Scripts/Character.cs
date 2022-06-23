using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that represents a character.
 * </summary>
 */
public class Character : MonoBehaviour
{
    private Movement movement;
    private Health health;
    private List<StatusEffect> statusEffects;

    //events
    public delegate void ChangeEventHandler<T, U>(T sender, U eventArgs);
    public event ChangeEventHandler<Character, EventArgs> HealthChange;
    public event ChangeEventHandler<Character, EventArgs> Death;
    public event ChangeEventHandler<Character, EventArgs> MoveSpeedChange;

    // status effects
    private bool isSlowed;
    private bool isStunned;

    public bool IsSlowed { get { return isSlowed; } set { isSlowed = value; } }
    public bool IsStunned { get { return isStunned; } set { isStunned = value; } }

    public virtual void Awake()
    {
        this.movement = gameObject.GetComponent<Movement>();
        this.health = gameObject.GetComponent<Health>();
        this.statusEffects = new List<StatusEffect>();
        this.health.HealthChange += OnCharacterHealthChange;
        this.health.DiedInfo += OnCharacterDeath;
        this.movement.MoveSpeedChange += OnCharacterMoveSpeedChange;
    }

    public virtual void OnDestroy()
    {
        this.health.HealthChange -= OnCharacterHealthChange;
        this.health.DiedInfo -= OnCharacterDeath;
        this.movement.MoveSpeedChange -= OnCharacterMoveSpeedChange;
    }

    #region Movement Methods
    public float GetMoveSpeed()
    {
        return this.movement.MoveSpeed;
    }
    public void SetMoveSpeed(float movespeed)
    {
        this.movement.SetMoveSpeed(movespeed);
    }

    public void ResetMoveSpeed()
    {
        this.movement.ResetMoveSpeed();
    }

    public float GetBaseMoveSpeed()
    {
        return this.movement.GetBaseMoveSpeed();
    }

    public void DisableMovement()
    {
        this.movement.enabled = false;
    }

    public void EnableMovement()
    {
        this.movement.enabled = true;
    }

    private void OnCharacterMoveSpeedChange(Movement sender, EventArgs e)
    {
        MoveSpeedChange?.Invoke(this, e);
    }
    #endregion

    #region Health Methods
    public float GetCurrentHealth()
    {
        return this.health.CurrentHealth;
    }

    public void SetCurrentHealth(float value)
    {
        this.health.CurrentHealth = value;
    }

    public float GetMaxHealth()
    {
        return this.health.MaxHealth;
    }

    public void SetMaxHealth(float value)
    {
        this.health.MaxHealth = value;
    }

    public void TakeDamage(float damage)
    {
        this.health.TakeDamage(damage);
    }

    private void OnCharacterHealthChange(Health sender, EventArgs e)
    {
        HealthChange?.Invoke(this, e);
    }

    private void OnCharacterDeath()
    {
        Death?.Invoke(this, EventArgs.Empty);
    }
    #endregion

    public IEnumerator HandleStatusEffect(StatusEffect statusEffect)
    {
        List<StatusEffect> existingStatusEffects = this.statusEffects.FindAll(x => x.Equals(statusEffect));
        if (existingStatusEffects.Count != 0)
        {
            foreach (StatusEffect existingStatusEffect in existingStatusEffects)
            {
                statusEffect.Potency =
                    Math.Max(statusEffect.Potency, existingStatusEffect.Potency);
                existingStatusEffect.Deactivate();
                yield return new WaitUntil(() => !existingStatusEffect.IsActive());
            }
        }
        StartCoroutine(statusEffect.StartEffect(this));
        this.statusEffects.Add(statusEffect);
        statusEffect.StatusEffectEnd += (sender, e) =>
        {
            if (this.statusEffects.Contains(statusEffect))
            {
                this.statusEffects.Remove(statusEffect);
                Destroy(statusEffect.gameObject);
            }
        };
        yield return null;
    }
}
