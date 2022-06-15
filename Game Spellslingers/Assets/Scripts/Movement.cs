using System;
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
    private Vector2 movement;

    public delegate void MovementChangeEventHandler<T, U>(T sender, U eventArgs);
    public event MovementChangeEventHandler<Movement, EventArgs> MoveSpeedChange;

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

        this.anim = GetComponent<Animator>();
        this.baseMoveSpeed = moveSpeed;
    }

    public virtual void Update()
    {
        AnimateMovement();
    }

    private void FixedUpdate()
    {
        UpdatePosition();
    }

    public float MoveSpeed { get { return this.moveSpeed; } set { this.moveSpeed = value; } }

    public float GetMoveSpeed()
    {
        return this.moveSpeed;
    }

    public void ResetMoveSpeed()
    {
        this.moveSpeed = this.baseMoveSpeed;
        OnMoveSpeedChange();
    }

    public float GetBaseMoveSpeed()
    {
        return this.baseMoveSpeed;
    }

    public void SetMoveSpeed(float movespeed)
    {
        this.moveSpeed = movespeed;
        OnMoveSpeedChange();
    }

    public void SetX(float f) { this.movement.x = f; }

    public void SetY(float f) { this.movement.y = f; }

    public Rigidbody2D GetRb()
    {
        return this.rb;
    }

    private void AnimateMovement()
    {
        if (this.anim == null)
        {
            return;
        }
        if (this.movement.x > 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (this.movement.x < 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        this.anim.SetFloat("Speed", this.movement.sqrMagnitude);
    }

    /**
     * <summary>
     * Give the animator extra conditions to animate 
     * the character base on.
     * </summary>
     */
    public void SetAnimParam(string s, float value)
    {
        this.anim.SetFloat(s, value);
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

    protected virtual void OnMoveSpeedChange()
    {
        MoveSpeedChange?.Invoke(this, EventArgs.Empty);
    }
}
