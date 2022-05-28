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
    private List<IStatusEffect> statusEffects;
    public virtual void Awake()
    {
        this.movement = gameObject.GetComponent<Movement>();
        this.health = gameObject.GetComponent<Health>();
        this.avoid = gameObject.GetComponent<Avoidance>();
    }
    public Movement Movement { get { return this.movement; } }
    public Health Health { get { return this.health; } }
    public Avoidance Avoidance { get { return this.avoid; } }

    public IEnumerator HandleStatusEffect(IStatusEffect statusEffect)
    {
        /*
        Debug.Log("Got Effect" + statusEffect);
        IStatusEffect existingStatusEffect = 
            this.statusEffects.Find(x => x.Equals(statusEffect));
        Debug.Log(existingStatusEffect);
        if (existingStatusEffect == null)
        {
            Debug.Log("No existing effect");
            this.statusEffects.Add(statusEffect);
            statusEffect.Activate(this);
            yield return new WaitForSeconds(statusEffect.GetDuration());
            this.statusEffects.Remove(statusEffect);
        } else
        {

        }
        yield return null;
        */
        yield return null;
    }
}
