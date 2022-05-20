using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static int maxHealth = 100;

    Player() : base(10f, Player.maxHealth) { }

    void Update()
    {
        // Get user input 
        SetX(Input.GetAxisRaw("Horizontal"));
        SetY(Input.GetAxisRaw("Vertical"));
        AnimateMovement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    void FixedUpdate()
    {
        UpdatePosition();
    }
}
