using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class responsible for movement.
 * </summary>
 */
public class Movement : MonoBehaviour
{
    [SerializeField] public Animator anim;
    [SerializeField] private float moveSpeed;


    private float baseMoveSpeed;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Vector2 movement;

    /**
     * <summary>
     * Adds a rigidbody2D that conforms to 2D physics.
     * </summary>
     */
    private void Awake()
    {
        this.rb = gameObject.AddComponent<Rigidbody2D>();
        this.rb.gravityScale = 0;
        this.rb.bodyType = RigidbodyType2D.Dynamic;
        this.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        this.rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        this.sr = GetComponent<SpriteRenderer>();
        this.anim = GetComponent<Animator>();
        this.baseMoveSpeed = moveSpeed;
    }

    public virtual void Update()
    {
        if (this.anim != null)
        {
            AnimateMovement();
        }
    }

    private void FixedUpdate()
    {
        UpdatePosition();
    }

    public float GetMoveSpeed() 
    {
        return this.moveSpeed;
    }

    public void ResetMoveSpeed()
    {
        this.moveSpeed = this.baseMoveSpeed;
    }

    public float GetBaseMoveSpeed()
    {
        return this.baseMoveSpeed;
    }

    public void SetMoveSpeed(float movespeed)
    {
        this.moveSpeed = movespeed;
    }

    public void SetX(float f) { this.movement.x = f; }

    public void SetY(float f) { this.movement.y = f; }

    public Rigidbody2D GetRb()
    {
        return this.rb;
    }

    private void AnimateMovement()
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

    /**
     * <summary>
     * Ensures that the rigidbody is at the same position 
     * where the character is at.
     * </summary>
     */
    private void UpdatePosition()
    {
        rb.MovePosition(this.rb.position + 
            this.movement.normalized * this.moveSpeed * Time.fixedDeltaTime);
    }
}
