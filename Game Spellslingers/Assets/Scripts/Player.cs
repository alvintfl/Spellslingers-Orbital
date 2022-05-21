using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static int maxHealth = 100;
    private Character _c;
    public Player() : base(10f, Player.maxHealth) { }

    public static Player instance;

    public delegate void PlayerDied();
    public static event PlayerDied playerDiedInfo;


    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else 
        {
            instance = this;
        }
    }

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


    public void PlayerDiesEvent()
    {
        // check function is subscribed
        if (playerDiedInfo != null)
            Debug.Log("1212hi");
            playerDiedInfo();
    }

    public void CheckPlayerStatus() 
    {
        // health hits zero
        if (CurrentHealth <= 0) 
        {
            AnimateDeath();
            this.PlayerDiesEvent();
        }
    }


}

