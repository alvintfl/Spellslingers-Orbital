using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerHealth health;

    public static Player instance;

    public PlayerMovement Movement { get { return this.movement; } }
    public PlayerHealth Health { get { return this.health; } }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            gameObject.AddComponent<GameObjectAutoAdd>();
        }
        this.movement = gameObject.GetComponent<PlayerMovement>();
        this.health = gameObject.GetComponent<PlayerHealth>();
    }
}
