using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;

    private Vector2 movement;
    [SerializeField] private float moveSpeed;
    private int currentHealth;

    public Character(float moveSpeed, int currentHealth)
    {
        this.moveSpeed = moveSpeed;
        this.currentHealth = currentHealth;
    }

    public virtual void Awake()
    {
        this.rb = gameObject.AddComponent<Rigidbody2D>();
        this.rb.gravityScale = 0;
    }
    public void SetX(float f) { this.movement.x = f; }
    public void SetY(float f) { this.movement.y = f; }
    public int CurrentHealth { get { return currentHealth; } set { this.currentHealth = value; } }

    public void AnimateMovement()
    {
        this.animator.SetFloat("Horizontal", this.movement.x);
        this.animator.SetFloat("Vertical", this.movement.y);
        this.animator.SetFloat("Speed", this.movement.sqrMagnitude);
    }

    public void UpdatePosition()
    {
        rb.MovePosition(this.rb.position + 
            this.movement.normalized * this.moveSpeed * Time.fixedDeltaTime);
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
