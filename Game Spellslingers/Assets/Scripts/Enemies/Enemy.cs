using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * <summary>
 * A class that represents an enemy.
 * </summary>
 */
public class Enemy : Character
{
    private float enemyDamage;
    private int exp;

    /** 
     * <summary>
     * A bool to check if the collided object is still in contact
     * after the initial onCollisionEnter2D.
     * </summary>
     */
    private bool IsCollidedStay;
    public static event EventHandler<DropExpEventArgs> DropExp;

    private void Start()
    {
        this.Health.DiedInfo += OnDropExp;
    }

    public Enemy(float ed, int exp)
    {
        this.enemyDamage = ed;
        this.exp = exp;
    }

    /**
     * <summary>
     * Check if the game object that collided with this game object
     * is the player. If yes, check the player's avoidance chance. 
     * If true, the player takes no damage, else deal damage to the
     * player equals to this enemy's damage.
     * </summary>
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!Player.instance.Avoidance.avoidRoll())
            {
                Player.instance.Health.TakeDamage(this.enemyDamage);
            }
        }
    }

    /**
     * <summary>
     * For every second in contact with this game object,
     * check if the game object in contact with this game object
     * is the player. If yes, check the player's avoidance chance. 
     * If true, the player takes no damage, else deal damage to the
     * player equals to this enemy's damage.
     * </summary>
     */
    private IEnumerator OnCollisionStay2D(Collision2D collision)
    {
        if (!IsCollidedStay && collision.gameObject.CompareTag("Player"))
        {
            IsCollidedStay = true;
            while (IsCollidedStay)
            {
                if (!Player.instance.Avoidance.avoidRoll()) 
                { 
                    Player.instance.Health.TakeDamage(this.enemyDamage);
                }
                yield return new WaitForSeconds(1);
            }
        }
        yield return null;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            IsCollidedStay = false;
        }
    }

    protected virtual void OnDropExp()
    {
        DropExpEventArgs args = new DropExpEventArgs();
        args.Exp = this.exp;
        DropExp?.Invoke(this, args);
        Die();
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}

/** 
 * <summary>
 * Class that stores the exp of an enemy
 * for the DropExp event.
 * </summary>
 */
public class DropExpEventArgs : EventArgs
{
    public int Exp { get; set; }
}
