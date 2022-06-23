using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonewtShoot : Shoot
{
    private Vector3 right;
    private Vector3 left;
    private HashSet<Vector2> seen;

    /**
     * <summary>
     * Bool to determine which leg to fire from.
     * </summary>
     */
    private bool isRight;

    public override void Start()
    {
        base.Start();
        this.right = new Vector3(0.5f, -1, 0);
        this.left = new Vector3(-0.5f, -1, 0);
        this.isRight = true;
        this.seen = new HashSet<Vector2>();
    }

    private void StartFire()
    {
        StartCoroutine(Fire());
    }

    /**
     * <summary>
     * </summary>
     */
    public override IEnumerator Fire()
    {
        Vector3 feetPosition;
        float x;
        float y;
        float increase;

        #region Initialise variables based on feet animation.
        if (this.isRight)
        {
            this.isRight = false;
            feetPosition = this.right;
            x = -1f;
            y = -1f;
            increase = 0.4f;
        } else
        {
            this.isRight = true;
            feetPosition = this.left;
            x = -0.8f;
            y = -0.8f;
            increase = 0.4f;
        }
        #endregion

        // If the game object is rotated then flip leg firing position as well.
        if (!gameObject.transform.rotation.Equals(Quaternion.identity))
        {
            feetPosition = feetPosition == this.right ? left : right;
        }

        for (float i = x; i <= 1; i += increase)
        {
            for (float j = y; j <= 1; j += increase)
            {
                #region Direction to fire projectile.

                // Don't fire projectiles with no direction
                if (i == 0 && j == 0)
                {
                    continue;
                }
                Vector2 direction = new Vector2(i, j);
                direction.Normalize();

                // Prevent projectiles from stacking on one another
                if (this.seen.Contains(direction))
                {
                    continue;
                }
                #endregion

                GameObject projectile = GetProjectile();
                if (projectile != null)
                {
                    #region Position and speed variables to fire projectile.
                    projectile.transform.position = 
                        gameObject.transform.position + feetPosition;
                    Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                    float speed = projectile.GetComponent<Projectile>().Speed;
                    rb.AddForce(direction * speed, ForceMode2D.Impulse);
                    this.seen.Add(direction);
                    #endregion
                }
            }
        }
        this.seen.Clear();
        yield return null;
    }
}
