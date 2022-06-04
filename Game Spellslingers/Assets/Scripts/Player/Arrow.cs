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
    [SerializeField] private Sprite arrowSprite;
    [SerializeField] private Sprite frostArrow;
    private SpriteRenderer spriteRenderer;
    private static int damage = 10;
    private static int pierceMax = 1;
    private int pierceCount = 0;


    /**
     * <summary>
     * A float to multiply damage.
     * </summary>
     */
    private static float damageMulti = 1;
    public static float DamageMulti { get { return damageMulti; } }

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
    private static bool isFrostArrowActive = false;

    /**
     * <summary>
     * A bool to let all arrows know they
     * can stun.
     * </summary>
     */

    private static bool isStunActive  = false;


    public Arrow() : base(15f, 250f) { }


    private void Awake()
    {
        gameObject.GetComponent<Lifesteal>().enabled = false;
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        this.spriteRenderer.sprite = this.arrowSprite;
    }

    private void Start()
    {
        this.Collided += ResetPierce;
    }

    private void OnEnable()
    {
        if (isFrostArrowActive)
        {
            this.spriteRenderer.sprite = this.frostArrow;
        }
    }

    private void OnDestroy()
    {
        this.Collided -= ResetPierce;
    }

    public static int getPierceMax()
    {
        return pierceMax;
    }
    public static void setPierceMax(int value)
    {
        pierceMax = value;
    }

    public static void SetDamageMulti(float mult)
    {
        Arrow.damageMulti = mult;
    }

    public static void IncreaseDamage(int damage)
    {
        Arrow.damage += damage;
    }

    public override int GetDamage()
    {
        return Arrow.damage;
    }



    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            if (pierceCount >= pierceMax)
            {
                pierceCount = 0;
                base.OnTriggerEnter2D(collider);
            } else
            {
                pierceCount += 1;
            }
            if (collider.gameObject != null)
            {
                Health enemyHealth = collider.gameObject.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(GetDamage() * DamageMulti);
                    print("damage multi = " + DamageMulti);
                    if (enemyHealth.CurrentHealth > 0)
                    {
                        SlowEnemy(collider);
                        StunEnemy(collider);
                    }
                }
            }
            Lifesteal();
        }
    }

    public static void ActivateLifeSteal()
    {
        Arrow.isLifestealActive = true;
    }
    public static void DeactivateLifeSteal()
    {
        Arrow.isLifestealActive = false;
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
    public static void DeactivateFrostArrow()
    {
        Arrow.isFrostArrowActive = false;
    }

    public static void ActivateStun()
    {
        Arrow.isStunActive = true;
    }
    public static void DeactivateStun()
    {
        Arrow.isStunActive = false;
    }


    private void SlowEnemy(Collider2D collider)
    {
        if(isFrostArrowActive)
        {
            FrostArrow frostArrow = Player.instance
                .gameObject.GetComponentInChildren<FrostArrow>(true);
            frostArrow.gameObject.SetActive(true);
            frostArrow.Slow(collider);
        }
    }

    private void StunEnemy(Collider2D collision)
    {
        if (isStunActive)
        {
            PlayerStun stun = Player.instance
                    .gameObject.GetComponentInChildren<PlayerStun>(true);
            stun.gameObject.SetActive(true);
            stun.Stun(collision);
        }

    }

    public static void ResetDamage()
    {
        Arrow.damage = 10;
    }

    public static void ResetPierceMax()
    {
        Arrow.pierceMax = 0;
    }

    private void ResetPierce(object sender, EventArgs e)
    {
        this.pierceCount = 0;
    }
}
