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

    private Vector3 target;
    public GameObject playerObject;

    private void Start()
    {
        this.playerObject = Player.instance.gameObject;
        this.poolSize = 16;
        this.rate = 2f;
        this.lastFirePoint = new float[] {0,0};
        this.projectileCount = 1;
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

        Player.instance.Health.DiedInfo += StopFiring;
        StartCoroutine("Fire");
    }
    private void Update()
    {
        UpdateLastFirePoint();
    }

    private void OnDestroy()
    {
        Player.instance.Health.DiedInfo -= StopFiring;
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            Dictionary<GameObject, bool> seen = new Dictionary<GameObject, bool>();
            for (int i = 0; i < this.projectileCount; i++)
            {
                GameObject projectile = this.projectilePool.Get();
                if (projectile != null)
                {
                    while (seen.ContainsKey(projectile))
                    {
                        projectile = this.projectilePool.Get();
                    }
                    // Position, direction and speed variables to fire projectile.
                    projectile.transform.position = this.firePoint.position;
                    Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                    float speed = projectile.GetComponent<Projectile>().Speed;

                    // Arranging the position of projectiles to be fired all at once.
                    float coordinate = (float) (Math.Ceiling((double) i / 2));
                    coordinate = i % 2 == 0 ? coordinate : -1 * coordinate;

                    target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                        Input.mousePosition.y, transform.position.z));

                    // add rotation for additional arrows
                    Vector3 difference = target - playerObject.transform.position;
                    float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                    rotationZ += (float)coordinate * 20;

                    float distance = difference.magnitude;
                    
                    // rotate direction of force added to additional arrows
                    Vector2 direction = difference / distance;
                    direction = rotate(direction, coordinate * 20 * Mathf.Deg2Rad);
                    direction.Normalize();
                    projectile.transform.rotation = Quaternion.Euler(0, 0, rotationZ + 220);
                    projectile.transform.position = playerObject.transform.position;
                    rb.AddForce(direction * speed, ForceMode2D.Impulse);
                }
                seen.Add(projectile, true);
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

    private void StopFiring()
    {
        StopAllCoroutines();
    }

    public void AddProjectiles()
    {
        this.projectileCount++;
        playerObject.GetComponent<Archer>().Projectiles += 1;
    }

    public void IncreaseRate(float decrease)
    {
        this.rate -= decrease;
    }

    public void IncreaseDamage(int damage)
    {
        this.projectilePrefab.GetComponent<Projectile>().IncreaseDamage(damage);
    }

    public static Vector2 rotate(Vector2 v, float delta)
    {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }

}
