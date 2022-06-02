using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Shoot : MonoBehaviour
{
    /**
     * <summary>
     * The projectile to fire.
     * </summary>
     */
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int poolSize;

    /**
     * <summary>
     * The object pool that stores all the projectiles.
     * </summary>
     */
    private ObjectPool<GameObject> projectilePool;

    public virtual void Start()
    {
        this.projectilePool = new ObjectPool<GameObject>(
        () => {
            GameObject projectileObject = Instantiate(projectilePrefab);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Collided +=
                (sender, e) => this.projectilePool.Release(projectileObject);
            projectile.FirePoint = gameObject.transform;
            return projectileObject;
        },
        x => x.SetActive(true),
        x => x.SetActive(false),
        x => Destroy(x),
        false, this.poolSize, this.poolSize + 1);

        GetComponent<Character>().Health.DiedInfo += StopFiring;
    }

    public abstract IEnumerator Fire();

    public GameObject GetProjectile()
    {
        return this.projectilePool.Get();
    }

    private void OnDestroy()
    {
        GetComponent<Character>().Health.DiedInfo -= StopFiring;
    }

    private void StopFiring()
    {
        StopAllCoroutines();
    }
}
