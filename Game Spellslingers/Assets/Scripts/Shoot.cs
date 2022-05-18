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
    private float rate;
    private float[] lastFirePoint;

    private void Start()
    {
        this.poolSize = 10;
        this.maxRange = 7;
        this.rate = 2f;
        this.lastFirePoint = new float[] {0,0};
        this.activeProjectiles = new Queue<GameObject>();
        this.projectilePool = new ObjectPool<GameObject>(
        () => Instantiate(projectilePrefab),
        x => {
            x.SetActive(true);
            activeProjectiles.Enqueue(x);
        },
        x => x.SetActive(false),
        x => Destroy(x),
        false, this.poolSize, this.poolSize + 1);

        StartCoroutine(Fire());
    }
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (x == 0 && y == 0)
        {
            return;
        } else
        {
            this.lastFirePoint[0] = x;
            this.lastFirePoint[1] = y;
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

    private void FixedUpdate()
    {
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            GameObject projectile = this.projectilePool.Get();
            if (projectile != null)
            {
                projectile.transform.position = this.firePoint.position;
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                float speed = projectile.GetComponent<Projectile>().Speed;
                float x = Input.GetAxisRaw("Horizontal");
                float y = Input.GetAxisRaw("Vertical");
                Vector3 v = new Vector3(1,0,0);
                if (x == 0 && y == 0)
                {
                    x = lastFirePoint[0];
                    y = lastFirePoint[1];
                }
                if (y > 0)
                {
                    if (x > 0)
                    {
                        projectile.transform.eulerAngles = new Vector3(0,0,270f);
                        v = new Vector3(1, 1, 0);
                    } else if (x < 0)
                    {
                        projectile.transform.eulerAngles = new Vector3(0,0,360f);
                        v = new Vector3(-1, 1, 0);
                    } else
                    {
                        projectile.transform.eulerAngles = new Vector3(0,0,315f);
                        v = new Vector3(0, 1, 0);
                    }
                } else if (y < 0)
                {
                    if (x > 0)
                    {
                        projectile.transform.eulerAngles = new Vector3(0,0,180f);
                        v = new Vector3(1, -1, 0);
                    } else if (x < 0)
                    {
                        projectile.transform.eulerAngles = new Vector3(0,0,90f);
                        v = new Vector3(-1, -1, 0);
                    } else
                    {
                        projectile.transform.eulerAngles = new Vector3(0,0,135f);
                        v = new Vector3(0, -1, 0);
                    }
                } else
                {
                    if (x >= 0)
                    {
                        projectile.transform.eulerAngles = new Vector3(0,0,225f);
                        v = new Vector3(1, 0, 0);
                    } else if (x < 0)
                    {
                        projectile.transform.eulerAngles = new Vector3(0,0,45f);
                        v = new Vector3(-1, 0, 0);
                    }
                }
                rb.AddForce(v.normalized * speed, ForceMode2D.Impulse);
                yield return new WaitForSeconds(this.rate);
            }
            yield return null;
        }
    }
}
