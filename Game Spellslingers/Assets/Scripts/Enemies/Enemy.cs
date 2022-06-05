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
    [SerializeField] private float damage;
    [SerializeField] private int exp;

    /** 
     * <summary>
     * A bool to check if the collided object is still in contact
     * after the initial onCollisionEnter2D.
     * </summary>
     */
    private bool IsCollidedStay;
    public static event EventHandler<DropExpEventArgs> DropExp;

    public virtual void Start()
    {
        this.Health.DiedInfo += OnDropExp;
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
    public virtual IEnumerator OnCollisionStay2D(Collision2D collision)
    {
        if (!IsCollidedStay && collision.gameObject.CompareTag("Player"))
        {
            IsCollidedStay = true;
            while (IsCollidedStay)
            {
                if (!Player.instance.Avoidance.avoidRoll())
                {
                    Player.instance.Health.TakeDamage(this.damage);
                }
                else if (Player.instance.Avoidance.GetRestoreOnAvoid())
                {
                    Player.instance.Health.TakeDamage(-10);
                }
                yield return new WaitForSeconds(1);
            }
        }
        yield return null;
    }

    public virtual void OnCollisionExit2D(Collision2D collision)
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
