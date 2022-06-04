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
    private Avoidance avoid;
    private List<StatusEffect> statusEffects;

    // status effects
    private bool isSlowed;
    private bool isStunned; 

    public bool IsSlowed { get { return isSlowed; } set { isSlowed = value; } }
    public bool IsStunned { get { return isStunned; } set { isStunned = value;  } }


    public virtual void Awake()
    {
        this.movement = gameObject.GetComponent<Movement>();
        this.health = gameObject.GetComponent<Health>();
        this.avoid = gameObject.GetComponent<Avoidance>();
        this.statusEffects = new List<StatusEffect>();
    }

    public Movement Movement { get { return this.movement; } }
    public Health Health { get { return this.health; } }
    public Avoidance Avoidance { get { return this.avoid; } }

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
