using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that represents the sandstorm 
 * around the dust elemental.
 * </summary>
 */
public class EnemyDustElementalAtttack : MonoBehaviour
{
    private int damage;

    /** 
     * <summary>
     * A bool to check if the triggered object is still in contact
     * after the initial onTriggerEnter2D.
     * </summary>
     */
    private bool IsTriggeredStay;

    private void Start()
    {
        this.damage = 10;
        this.IsTriggeredStay = false;
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
    private IEnumerator OnTriggerStay2D(Collider2D collider)
    {
        if (!IsTriggeredStay && collider.gameObject.CompareTag("Player"))
        {
            IsTriggeredStay = true;
            while (IsTriggeredStay)
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

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            IsTriggeredStay = false;
        }
    }
}
