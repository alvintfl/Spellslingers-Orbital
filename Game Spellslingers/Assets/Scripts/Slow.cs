using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : StatusEffect
{
    public Slow() : base(0.4f, 2f) { }

    public override IEnumerator StartEffect(Character character)
    {
        if (character != null)
        {
            Activate();
            float startTime = Time.time;

            // Slow the character
            Movement movement = character.Movement;
            float slowedMovespeed = movement.GetBaseMoveSpeed() * (1 - Potency);
            movement.SetMoveSpeed(slowedMovespeed);
            Debug.Log(movement.GetMoveSpeed());

            // Wait until slow duration is up or the slow is deactivated
            yield return new WaitWhile(() => Time.time - startTime < Duration && IsActive());

            // Set movespeed back to normal
            movement.ResetMoveSpeed();
            Debug.Log(movement.GetMoveSpeed());
            Deactivate();
            OnStatusEffectEnd(EventArgs.Empty);
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
