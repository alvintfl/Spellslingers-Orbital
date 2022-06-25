using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningCircle : MonoBehaviour
{
    public delegate void SummonEventHandler<T, U>(T sender, U eventArgs);
    public static event SummonEventHandler<SummoningCircle, EventArgs> Summon;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            OnTrigger();
        }
    }
    protected virtual void OnTrigger()
    {
        Summon?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}
