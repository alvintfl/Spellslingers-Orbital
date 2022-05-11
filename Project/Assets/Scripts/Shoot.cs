using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    private ObjectPool<GameObject> projectilePool;

    private void Start()
    {
        this.projectilePool = new ObjectPool<GameObject>(
        () => {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.GetComponent<Projectile>().ProjectilePool = this.projectilePool;
            return projectile;
        },
        x => x.SetActive(true),
        x => x.SetActive(false),
        x => Destroy(x),
        false, 5, 6);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    public void Fire()
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
