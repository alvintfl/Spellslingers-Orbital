using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    //public static int maxHealth = 100;
    /*
    public delegate void PlayerDied();
    public static event PlayerDied playerDiedInfo;
    private Animator anim;
    */



    //public PlayerHealth() : base(PlayerHealth.maxHealth) { }

    /*
    private void Awake() {
        this.anim = GetComponent<Animator>();
    }
    */
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    /*
    public void PlayerDiesEvent()
    {
        // check function is subscribed
        if (playerDiedInfo != null)
    
            playerDiedInfo();
    }

    public void AnimateDeath()
    {
        this.anim.SetTrigger("Death");
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

    protected override void OnHealthChange(EventArgs e)
    {
        base.OnHealthChange(e);
        CheckPlayerStatus();
    }
    */

}
