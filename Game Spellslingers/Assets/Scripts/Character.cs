using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Vector2 movement;
    private float moveSpeed = 0;
    public float currentHealth = 100f;


    public event EventHandler HealthChange;
    public Character(float moveSpeed, float currentHealth)
    {
        this.moveSpeed = moveSpeed;
        this.currentHealth = currentHealth;
    }


    public float GetMoveSpeed() 
    {
        return this.moveSpeed;
    }

    public void SetMoveSpeed(float movespeed) 
    {
        this.moveSpeed += movespeed;
    }


    public virtual void Awake()
    {

        this.rb = gameObject.AddComponent<Rigidbody2D>();
        this.rb.gravityScale = 0;
        this.rb.bodyType = RigidbodyType2D.Dynamic;
        this.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        this.rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        this.sr = GetComponent<SpriteRenderer>();
        this.anim = GetComponent<Animator>();
    }
    
    public Rigidbody2D GetRb() {
        return this.rb;
    }

    public void SetX(float f) { this.movement.x = f; }
    public void SetY(float f) { this.movement.y = f; }
    public float CurrentHealth { 
        get { return currentHealth; } 
        set 
        { 
            this.currentHealth = value;
            OnHealthChange(EventArgs.Empty);
        } 
    }

    public void AnimateMovement()
    {
        if (this.movement.x > 0) 
        {
            sr.flipX = false;
        }
        if (this.movement.x < 0) 
        {
            sr.flipX = true;
        }
        this.anim.SetFloat("Horizontal", this.movement.x);
        this.anim.SetFloat("Vertical", this.movement.y);
        this.anim.SetFloat("Speed", this.movement.sqrMagnitude);
    }

    public void AnimateDeath()
    {
        if (this.movement.x > 0)
        {
            sr.flipX = false;
        }
        if (this.movement.x < 0)
        {
            sr.flipX = true;
        }
        this.anim.SetTrigger("Death");
    }


    public void UpdatePosition()
    {
        rb.MovePosition(this.rb.position + 
            this.movement.normalized * this.moveSpeed * Time.fixedDeltaTime);
    }

    public virtual void TakeDamage(float damage)
    {
        Debug.Log("taking dmg");
        currentHealth -= damage;
        OnHealthChange(EventArgs.Empty);
    }
    private void OnHealthChange(EventArgs e)
    {
        HealthChange?.Invoke(this, e);
    }

}
