using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;

    private Vector2 movement;
    private float moveSpeed = 0;
    private float currentHealth = 100f;

    public Character(float moveSpeed, float currentHealth)
    {
        this.moveSpeed = moveSpeed;
        this.currentHealth = currentHealth;
    }

    public float GetMoveSpeed() {
        return this.moveSpeed;
    }

    public virtual void Awake()
    {
        this.rb = gameObject.AddComponent<Rigidbody2D>();
        this.rb.gravityScale = 0;
        this.rb.bodyType = RigidbodyType2D.Dynamic;
        this.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public Rigidbody2D GetRb() {
        return this.rb;
    }

    public void SetX(float f) { this.movement.x = f; }
    public void SetY(float f) { this.movement.y = f; }
    public float CurrentHealth { get { return currentHealth; } set { this.currentHealth = value; } }

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

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

}
