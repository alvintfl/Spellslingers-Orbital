using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    private float speed;
    public Projectile(float speed)
    {
        this.speed = speed; 
    }

    public float Speed { get { return speed; } }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }
}
