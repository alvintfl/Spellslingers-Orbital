using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    private float speed;
    private ObjectPool<GameObject> projectilePool;
    public Projectile(float speed)
    {
        this.speed = speed; 
    }

    void OnEnable()
    {
        Invoke("Release", 2f);
    }

    public float Speed { get { return speed; } }
    public ObjectPool<GameObject> ProjectilePool { set { projectilePool = value; } }

    private void Release()
    {
        if (gameObject != null && gameObject.activeSelf)
        {
            this.projectilePool.Release(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Release();
    }
}
