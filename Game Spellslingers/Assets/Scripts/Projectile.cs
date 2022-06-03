using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that represents a projectile.
 * </summary>
 */
public abstract class Projectile : MonoBehaviour
{
    private Transform firePoint;
    private float speed;
    private float maxRange;
    public event EventHandler Collided;

    public Projectile(float speed, float maxRange)
    {
        this.speed = speed;
        this.maxRange = maxRange;
    }

    private void Update()
    {
        AtMaxRange();
    }

    public abstract void IncreaseDamage(int damage);
    public abstract void SetDamageMulti(int mult);

    public abstract int GetDamage();
    public float Speed { get { return this.speed; } }

    public Transform FirePoint { set { this.firePoint = value; } }

    /**
     * <summary>
     * Returns the projectile back to the object pool
     * when it is too far away from the original 
     * position it was fired from.
     * </summary>
     */
    public void AtMaxRange()
    {
        if (this.firePoint != null && gameObject.activeSelf && 
                (Math.Abs(gameObject.transform.position.x - this.firePoint.position.x) > this.maxRange ||
                 Math.Abs(gameObject.transform.position.y - this.firePoint.position.y) > this.maxRange))
        {
            OnCollided(EventArgs.Empty);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.activeSelf)
        {
            OnCollided(EventArgs.Empty);
        }
    }

    protected virtual void OnCollided(EventArgs e)
    {
        Collided?.Invoke(this, e);
    }
}
