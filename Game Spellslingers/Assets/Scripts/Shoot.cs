using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    private Queue<GameObject> activeProjectiles;
    private ObjectPool<GameObject> projectilePool;
    private int poolSize;
    private int maxRange;

    private void Start()
    {
        this.poolSize = 10;
        this.maxRange = 7;
        this.activeProjectiles = new Queue<GameObject>();
        this.projectilePool = new ObjectPool<GameObject>(
        () => Instantiate(projectilePrefab),
        x => {
            x.SetActive(true);
            activeProjectiles.Enqueue(x);
        },
        x => {
            x.SetActive(false);
        },
        x => Destroy(x),
        false, this.poolSize, this.poolSize + 1);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
        
        if (this.activeProjectiles.Count > this.poolSize)
        {
            GameObject projectile = this.activeProjectiles.Dequeue();
            if (projectile.activeSelf && 
                (projectile.transform.position.x - this.firePoint.position.x > this.maxRange ||
                 projectile.transform.position.y - this.firePoint.position.y > this.maxRange
                 ))
            {
                this.projectilePool.Release(projectile);
            } else
            {
                this.activeProjectiles.Enqueue(projectile);
            }
        } else if (this.activeProjectiles.Count > 0)
        {
            GameObject projectile = this.activeProjectiles.Dequeue();
            if (!projectile.activeSelf)
            {
                this.projectilePool.Release(projectile);
            } else
            {
                this.activeProjectiles.Enqueue(projectile);
            }
        }
    }

    private void Fire()
    {
        GameObject projectile = projectilePool.Get();
        if (projectile != null)
        {
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation;
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            float speed = projectile.GetComponent<Projectile>().Speed;
            rb.AddForce((-1 * this.firePoint.right + this.firePoint.up) * speed, ForceMode2D.Impulse);
        }
    }
}
