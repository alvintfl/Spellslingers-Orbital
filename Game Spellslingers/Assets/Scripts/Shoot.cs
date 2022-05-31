using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/**
 * <summary>
 * A class that is resposible for shooting.
 * </summary>
 */
public class Shoot : MonoBehaviour
{
    /**
     * <summary>
     * The position the projectile was fired from.
     * </summary>
     */
    [SerializeField] private Transform firePoint;

    /**
     * <summary>
     * The projectile to fire.
     * </summary>
     */
    [SerializeField] private GameObject projectilePrefab;

    /**
     * <summary>
     * The object pool that stores all the projectiles.
     * </summary>
     */
    private ObjectPool<GameObject> projectilePool;
    private int poolSize;

    private int projectileCount;
    private float rate;
    private WaitForSeconds wait;

    private Vector3 target;
    private GameObject playerObject;

    private void Start()
    {
        this.playerObject = Player.instance.gameObject;
        this.poolSize = 16;
        this.rate = 1f;
        this.wait = new WaitForSeconds(this.rate);
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

    private void OnDestroy()
    {
        Player.instance.Health.DiedInfo -= StopFiring;
    }

    /**
     * <summary>
     * Fire a projectile based on where the cursor
     * is pointing at.
     * </summary>
     */
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
                    direction = Rotate(direction, coordinate * 20 * Mathf.Deg2Rad);
                    direction.Normalize();
                    projectile.transform.rotation = Quaternion.Euler(0, 0, rotationZ + 180);
                    projectile.transform.position = playerObject.transform.position;
                    rb.AddForce(direction * speed, ForceMode2D.Impulse);
                }
                seen.Add(projectile, true);
            }
            yield return this.wait;
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
        this.wait = new WaitForSeconds(this.rate);
    }

    public void IncreaseDamage(int damage)
    {
        this.projectilePrefab.GetComponent<Projectile>().IncreaseDamage(damage);
    }

    private Vector2 Rotate(Vector2 v, float delta)
    {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }
}
