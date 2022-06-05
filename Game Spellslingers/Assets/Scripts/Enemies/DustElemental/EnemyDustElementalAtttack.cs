using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDustElementalAtttack : MonoBehaviour
{
    private int damage;

    /** 
     * <summary>
     * A bool to check if the collided object is still in contact
     * after the initial onCollisionEnter2D.
     * </summary>
     */
    private bool IsCollidedStay;

    private void Start()
    {
        this.damage = 10;
        this.IsCollidedStay = false;
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
    public virtual IEnumerator OnTriggerStay2D(Collider2D collider)
    {
        if (!IsCollidedStay && collider.gameObject.CompareTag("Player"))
        {
            IsCollidedStay = true;
            while (IsCollidedStay)
            {
                if (!Player.instance.Avoidance.avoidRoll()) 
                { 
                    Player.instance.Health.TakeDamage(this.damage);
                }
                yield return new WaitForSeconds(1);
            }
        }
        yield return null;
    }

    public virtual void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            IsCollidedStay = false;
        }
    }
}
