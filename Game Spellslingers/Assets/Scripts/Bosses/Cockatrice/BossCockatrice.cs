using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCockatrice : Enemy
{
    // to do:
    /*
     * make cockatrice move to player, but not too close
     * create projectiles for claw cast, storm call, frost breath/ice beam
     * 
     */
    [SerializeField] private GameObject loot;

    public override void Awake()
    {
        base.Awake();
        Vector2 spawnPosition = new Vector2(-557, -11);
        gameObject.transform.position = spawnPosition;
        AudioManager.instance.Play("CockatriceSpawn");
    }

    public override void Die()
    {
        AudioManager.instance.Play("CockatriceDeath");
        // play death animation
        GameObject loot = Instantiate(this.loot);
        loot.transform.position = this.transform.position;
        Destroy(gameObject);
    }
    public override IEnumerator HandleStatusEffect(StatusEffect statusEffect)
    {
        if (statusEffect is Slow || statusEffect is Stun)
        {
            yield return null;
        } else
        {
            yield return base.HandleStatusEffect(statusEffect);
        }
    }
}
