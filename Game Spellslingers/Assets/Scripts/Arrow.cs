using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that represents an arrow.
 * </summary>
 */
public class Arrow : Projectile
{
    private static int damage = 10;
    private static int pierceMax = 0;

    /**
     * <summary>
     * A bool to let all arrows know they
     * can lifesteal.
     * </summary>
     */
    private static bool isLifestealActive = false;
    /**
     * <summary>
     * A bool to let the arrow know they 
     * have already activated lifesteal.
     * </summary>
     */
    private bool isLifestealActivated = false;

    /**
     * <summary>
     * A bool to let all arrows know they
     * can slow.
     * </summary>
     */
    private static bool isFrostArrowActive = true;

    private void Awake()
    {
        gameObject.GetComponent<Lifesteal>().enabled = false;
    }
    public static int getPierceMax()
    {
        return pierceMax;
    }
    public static void setPierceMax(int value)
    {
        pierceMax = value;
    }

    private int pierceCount = 0;
    public Arrow() : base(15f) { }

    public override void IncreaseDamage(int damage)
    {
        Arrow.damage += damage;
    }

    public override int GetDamage()
    {
        return Arrow.damage;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (pierceCount >= pierceMax)
            {
                pierceCount = 0;
                base.OnTriggerEnter2D(collision);
                if (collision.gameObject != null)
                {
                    Health enemyHealth = collision.gameObject.GetComponent<Health>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(GetDamage());
                    }
                }
            }
            else
            {
                pierceCount += 1;
                if (collision.gameObject != null)
                {
                    Health enemyHealth = collision.gameObject.GetComponent<Health>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(GetDamage());
                    }
                }
            }
            Lifesteal();
            FrostArrow(collision);
        }
    }

    public override void AtMaxRange()
    {
        if (this.firePoint != null && gameObject.activeSelf &&
                (Math.Abs(gameObject.transform.position.x - base.firePoint.position.x) > base.maxRange ||
                 Math.Abs(gameObject.transform.position.y - base.firePoint.position.y) > base.maxRange))
        {
            pierceCount = 0;
            OnCollided(EventArgs.Empty);
        }
    }

    public static void ActivateLifeSteal()
    {
        Arrow.isLifestealActive = true;
    }

    private void Lifesteal()
    {
        if(!isLifestealActivated && isLifestealActive)
        {
            isLifestealActivated = true;
            gameObject.GetComponent<Lifesteal>().enabled = true;
        }
    }
    public static void ActivateFrostArrow()
    {
        Arrow.isFrostArrowActive = true;
    }

    private void FrostArrow(Collider2D collision)
    {
        if(isFrostArrowActive)
        {
            FrostArrow frostArrow = Player.instance
                .gameObject.GetComponentInChildren<FrostArrow>(true);
            frostArrow.gameObject.SetActive(true);
            frostArrow.Slow(collision);
        }
    }
}
