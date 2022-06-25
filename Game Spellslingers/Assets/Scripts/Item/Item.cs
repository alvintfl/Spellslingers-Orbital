using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public delegate void PickUpEventHandler<T, U>(T sender, U eventArgs);
    public static event PickUpEventHandler<Item, EventArgs> PickUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnPickUp();
        }
    }

    protected virtual void OnPickUp()
    {
        PickUp?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}
