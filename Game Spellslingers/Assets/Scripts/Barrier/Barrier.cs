using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public delegate void CollideEventHandler<T, U>(T sender, U eventArgs);
    public static event CollideEventHandler<Barrier, EventArgs> Collide;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnCollide();
        }
    }

    protected virtual void OnCollide()
    {
        Collide?.Invoke(this, EventArgs.Empty);
    }
}
