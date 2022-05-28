using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour, IStatusEffect
{
    private static float slowAmount = 0.9f;
    private static float duration = 5f;

    public static void IncreaseSlow(float slow)
    {
        Slow.slowAmount += slow;
    }

    public static void IncreaseDuration(float duration)
    {
        Slow.duration += duration;
    }

    public float GetDuration()
    {
        return Slow.duration;
    }

    //private IEnumerator OnTriggerEnter2D(Collider2D collision)
    //private void OnTriggerEnter2D(Collider2D collision)
    public IEnumerator Activate(Character character)
    {
        Movement movement = character.Movement;
        float normalMovespeed = movement.GetMoveSpeed();
        float slowedMovespeed = normalMovespeed * (1 - Slow.slowAmount);
        movement.SetMoveSpeed(slowedMovespeed);
        Debug.Log(movement.GetMoveSpeed());
        yield return new WaitForSeconds(Slow.duration);

        movement.SetMoveSpeed(normalMovespeed);
        Debug.Log(movement.GetMoveSpeed());
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
