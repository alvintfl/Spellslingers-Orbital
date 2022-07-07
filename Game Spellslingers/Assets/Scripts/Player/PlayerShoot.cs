using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/**
 * <summary>
 * The shooting class for player.
 * </summary>
 */
public class PlayerShoot : Shoot
{
    private int projectileCount;
    private float rate;
    private HashSet<GameObject> seen;
    private WaitForSeconds wait;

    public delegate void ShootChangeEventHandler<T, U>(T sender, U eventArgs);
    public event ShootChangeEventHandler<PlayerShoot, EventArgs> PlayerShootChange;

    /**
    * <summary>
    * A bool to check if skills have randmised direction.
    * A float to assign a random angle between 0 and 360 to.
    * </summary>
    */
    private bool randomiseAim;
    private float randomRot;

    private void Awake()
    {
        this.rate = 1.8f;
        this.wait = new WaitForSeconds(this.rate);
        this.projectileCount = 1;
        this.randomiseAim = false;
        this.randomRot = 0;
        this.seen = new HashSet<GameObject>();
    }

    public override void Start()
    {
        base.Start();
        StartCoroutine(Fire());
    }

    /**
     * <summary>
     * Fire a projectile based on where the cursor
     * is pointing at.
     * </summary>
     */
    public override IEnumerator Fire()
    {
        while (true)
        {
            for (int i = 0; i < this.projectileCount; i++)
            {
                //check if projectiles have been randomised. If they are, randomise angles
                if (randomiseAim)
                {
                    randomRot = Random.Range(0, 360);
                }

                GameObject projectile = GetProjectile();
                if (projectile != null)
                {
                    while (this.seen.Contains(projectile))
                    {
                        projectile = GetProjectile();
                    }
                    // Position, direction and speed variables to fire projectile.
                    GameObject playerObject = Player.instance.gameObject;
                    projectile.transform.position = playerObject.transform.position;
                    Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                    float speed = projectile.GetComponent<Projectile>().Speed;

                    // Arranging the position of projectiles to be fired all at once.
                    float coordinate = (float)(Math.Ceiling((double)i / 2));
                    coordinate = i % 2 == 0 ? coordinate : -1 * coordinate;

                    Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                        Input.mousePosition.y, transform.position.z));

                    // add rotation for additional arrows
                    Vector3 difference = target - playerObject.transform.position;
                    float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                    rotationZ += (float)coordinate * 20;

                    float distance = difference.magnitude;

                    // rotate direction of force added to additional arrows
                    Vector2 direction = difference / distance;
                    direction = Rotate(direction, coordinate * 20 * Mathf.Deg2Rad + randomRot * Mathf.Deg2Rad);
                    direction.Normalize();
                    projectile.transform.rotation = Quaternion.Euler(0, 0, rotationZ + 180 + randomRot);
                    rb.AddForce(direction * speed, ForceMode2D.Impulse);
                }
                this.seen.Add(projectile);
            }
            this.seen.Clear();
            AudioManager.instance.Play("ArrowShoot");
            yield return this.wait;
        }
    }

    public void AddProjectiles(int num)
    {
        this.projectileCount += num;
        OnPlayerShootChange();
    }

    public int GetProjectileCount()
    {
        return this.projectileCount;
    }

    public void IncreaseRate(float secs)
    {
        this.rate -= secs;
        this.wait = new WaitForSeconds(this.rate);
        OnPlayerShootChange();
    }

    public void DecreaseRate(float secs)
    {
        this.rate += secs;
        this.wait = new WaitForSeconds(this.rate);
        OnPlayerShootChange();
    }

    public float GetRate()
    {
        return this.rate;
    }

    private Vector2 Rotate(Vector2 v, float delta)
    {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }

    public void ToggleRandomiseProjectiles()
    {
        if (randomiseAim)
        {
            randomiseAim = false;
        }
        else
        {
            randomiseAim = true;
        }
    }

    protected virtual void OnPlayerShootChange()
    {
        PlayerShootChange?.Invoke(this, EventArgs.Empty);
    }
}
