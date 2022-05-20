using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    private ObjectPool<GameObject> projectilePool;
    private int poolSize;
    private int projectileCount;
    private float rate;
    private float[] lastFirePoint;
    private float projectileSpacing;

    private void Start()
    {
        this.poolSize = 16;
        this.rate = 2f;
        this.lastFirePoint = new float[] {0,0};
        this.projectileCount = 1;
        this.projectileSpacing = 0.2f;
        this.projectilePool = new ObjectPool<GameObject>(
        () => {
            GameObject projectileObject = Instantiate(projectilePrefab);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Collided +=
                (sender, e) => this.projectilePool.Release(projectileObject);
            projectile.FirePoint = this.firePoint;
            return projectileObject;
        },
        x => x.SetActive(true),
        x => x.SetActive(false),
        x => Destroy(x),
        false, this.poolSize, this.poolSize + 1);

        StartCoroutine("Fire");
    }
    private void Update()
    {
        UpdateLastFirePoint();
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            for (int i = 0; i < this.projectileCount; i++)
            {
                GameObject projectile = this.projectilePool.Get();
                if (projectile != null)
                {
                    // Position, direction and speed variables to fire projectile.
                    projectile.transform.position = this.firePoint.position;
                    Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                    float speed = projectile.GetComponent<Projectile>().Speed;
                    Vector3 v = new Vector3(1, 0, 0);

                    // Arranging the position of projectiles to be fired all at once.
                    float coordinate = (float) (Math.Ceiling((double) i / 2) * this.projectileSpacing);
                    coordinate = i % 2 == 0 ? coordinate : -1 * coordinate;

                    float x = Input.GetAxisRaw("Horizontal");
                    float y = Input.GetAxisRaw("Vertical");
                    if (x == 0 && y == 0)
                    {
                        x = lastFirePoint[0];
                        y = lastFirePoint[1];
                    }
                    if (y > 0)
                    {
                        if (x > 0)
                        {
                            projectile.transform.eulerAngles = new Vector3(0, 0, 270f);
                            v = new Vector3(1, 1, 0);
                        }
                        else if (x < 0)
                        {
                            projectile.transform.eulerAngles = new Vector3(0, 0, 360f);
                            v = new Vector3(-1, 1, 0);
                        }
                        else
                        {
                            projectile.transform.eulerAngles = new Vector3(0, 0, 315f);
                            v = new Vector3(0, 1, 0);
                        }
                        projectile.transform.position += new Vector3((float) coordinate, 0, 0);
                    }
                    else if (y < 0)
                    {
                        if (x > 0)
                        {
                            projectile.transform.eulerAngles = new Vector3(0, 0, 180f);
                            v = new Vector3(1, -1, 0);
                        }
                        else if (x < 0)
                        {
                            projectile.transform.eulerAngles = new Vector3(0, 0, 90f);
                            v = new Vector3(-1, -1, 0);
                        }
                        else
                        {
                            projectile.transform.eulerAngles = new Vector3(0, 0, 135f);
                            v = new Vector3(0, -1, 0);
                        }
                        projectile.transform.position += new Vector3((float) coordinate, 0, 0);
                    }
                    else
                    {
                        if (x >= 0)
                        {
                            projectile.transform.eulerAngles = new Vector3(0, 0, 225f);
                            v = new Vector3(1, 0, 0);
                        }
                        else if (x < 0)
                        {
                            projectile.transform.eulerAngles = new Vector3(0, 0, 45f);
                            v = new Vector3(-1, 0, 0);
                        }
                        projectile.transform.position += new Vector3(0, (float) coordinate, 0);
                    }
                    rb.AddForce(v.normalized * speed, ForceMode2D.Impulse);
                }
            }
            yield return new WaitForSeconds(this.rate);
        }
    }

    public void UpdateLastFirePoint()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (!(x == 0 && y == 0))
        {
            this.lastFirePoint[0] = x;
            this.lastFirePoint[1] = y;
        }
    }

    public void AddProjectiles()
    {
        this.projectileCount++;
    }

    public void IncreaseRate(float decrease)
    {
        this.rate -= decrease;
    }

    public void IncreaseDamage(int damage)
    {
        this.projectilePrefab.GetComponent<Projectile>().IncreaseDamage(damage);
    }
}
