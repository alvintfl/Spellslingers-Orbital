using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static int maxHealth = 100;

    //private string ENEMY_TAG = "Enemy";

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
    /*
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        Debug.Log("????");
        if (collision.gameObject.CompareTag(ENEMY_TAG)) {
            Debug.Log("hi");
            TakeDamage(10);
        }
    }*/
}
