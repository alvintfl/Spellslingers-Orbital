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
    public delegate void ProjectileEventHandler<T, U>(T sender, U eventArgs);
    public event ProjectileEventHandler<Projectile, EventArgs> Collided;

    public Projectile(float speed, float maxRange)
    {
        this.speed = speed;
        this.maxRange = maxRange;
    }

    private void Update()
    {
        AtMaxRange();
    }

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
            (gameObject.transform.position - this.firePoint.position).sqrMagnitude > this.maxRange)
        {
            OnCollided(EventArgs.Empty);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collider)
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
