using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that represents an
 * item to progress the game.
 * </summary>
 */
public class Item : MonoBehaviour
{
    public delegate void PickUpEventHandler<T, U>(T sender, U eventArgs);
    public static event PickUpEventHandler<Item, EventArgs> PickUp;
    public static event PickUpEventHandler<Item, EventArgs> Spawned;

    private void Start()
    {
        OnSpawned();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnPickUp();
        }
    }

    protected virtual void OnPickUp()
    {
        AudioManager.instance.Play("ItemPickUp");
        PickUp?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }

    protected virtual void OnSpawned()
    {
        Spawned?.Invoke(this, EventArgs.Empty);
    }
}
