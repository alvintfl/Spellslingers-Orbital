using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Projectile : MonoBehaviour
{
    public Transform firePoint;
    public float speed;
    public int maxRange = 20;
    public event EventHandler Collided;

    public Projectile(float speed)
    {
        this.speed = speed; 
    }


    private void Update()
    {
        AtMaxRange();
    }

    public abstract void IncreaseDamage(int damage);

    public abstract int GetDamage();
    public float Speed { get { return this.speed; } }

    public Transform FirePoint { set { this.firePoint = value; } }


    public virtual void AtMaxRange()
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
