using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that represents the slow
 * status effect.
 * </summary>
 */
public class Slow : StatusEffect
{
    public Slow() : base(0.4f, 2f) { }

    /**
     * <summary>
     * Slow the character for a set amount of time.
     * </summary>
     */
    public override IEnumerator StartEffect(Character character)
    {
        if (character != null)
        {
            Activate();
            float startTime = Time.time;

            // Slow the character
            float slowedMovespeed = character.GetBaseMoveSpeed() * (1 - Potency);
            character.SetMoveSpeed(slowedMovespeed);

            // Wait until slow duration is up or the slow is deactivated
            yield return new WaitWhile(() => Time.time - startTime < Duration && IsActive());

            // Set movespeed back to normal
            character.ResetMoveSpeed();
            Deactivate();
            OnStatusEffectEnd(EventArgs.Empty);
            yield return new WaitForSeconds(Duration);
        }
        yield return null;
    }

    public override bool Equals(object other)
    {
        return other is Slow;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
