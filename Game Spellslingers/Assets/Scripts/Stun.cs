using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : StatusEffect
{
    public Stun() : base(1f, 1f) { }


    public override IEnumerator StartEffect(Character character)
    {
        if (character != null)
        {
            Activate();
            float startTime = Time.time;

            // Stun the character
            Movement movement = character.Movement;
            movement.SetMoveSpeed(0);

            // Wait until slow duration is up or the slow is deactivated
            yield return new WaitWhile(() => Time.time - startTime < Duration && IsActive());

            // Set movespeed back to normal
            movement.ResetMoveSpeed();
            Deactivate();
            OnStatusEffectEnd(EventArgs.Empty);
        }
        yield return null;
        
    }

    public override bool Equals(object other)
    {
        return other is Stun;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
