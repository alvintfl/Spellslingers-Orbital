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
        this.projectileSpacing = 0.2f;
        this.projectilePool = new ObjectPool<GameObject>(
        () => {
            Debug.Log("Making one arrow");
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
                    Vector3 v = new Vector3(1, 0, 0);

                    // Arranging the position of projectiles to be fired all at once.
                    float coordinate = (float) (Math.Ceiling((double) i / 2) * this.projectileSpacing);
                    coordinate = i % 2 == 0 ? coordinate : -1 * coordinate;


                    target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                        Input.mousePosition.y, transform.position.z));

                    Vector3 difference = target - playerObject.transform.position;
                    float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                    float distance = difference.magnitude;
                    Vector2 direction = difference / distance;
                    direction.Normalize();

                    projectile.transform.rotation = Quaternion.Euler(0, 0, rotationZ + 220);
                    projectile.transform.position = playerObject.transform.position;
                    rb.velocity = direction * 20f;
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
