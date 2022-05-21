using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static int maxHealth = 100;
    private Character _c;
    public Player(Character c) : base(10f, Player.maxHealth) {
        _c = c;
    
    }
    public delegate void PlayerDied();
    public static event PlayerDied playerDiedInfo;


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

    public static void CheckPlayerStatus() 
    {
        // health hits zero
        if (_c.currentHealth <= 0) 
        {
            Debug.Log("You have died!");
            // to make sure function is subscribed
            if (playerDiedInfo != null) 
                playerDiedInfo();     
        }
    }
}

