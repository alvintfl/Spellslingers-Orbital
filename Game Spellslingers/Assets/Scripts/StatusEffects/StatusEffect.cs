using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * The base class for status effects.
 * </summary>
 */
public abstract class StatusEffect : MonoBehaviour
{
    public event EventHandler StatusEffectEnd;
    private float potency;
    private float duration;

    /**
     * <summary>
     * Bool for whether this status effect is already
     * present on the character.
     * </summary>
     */
    private bool isActive;

    public StatusEffect(float potency, float duration)
    {
        this.potency = potency;
        this.duration = duration;
    }

    public abstract IEnumerator StartEffect(Character character);

    public float Potency { get { return this.potency; } set { this.potency = value; } }
    public float Duration { get { return this.duration; } set { this.duration = value; } }

    public bool IsActive()
    {
        return this.isActive;
    }

    public void Activate()
    {
        this.isActive = true;
    }

    public void Deactivate()
    {
        this.isActive = false;
    }

    public void OnStatusEffectEnd(EventArgs e)
    {
       StatusEffectEnd?.Invoke(this, e);
    }
}
