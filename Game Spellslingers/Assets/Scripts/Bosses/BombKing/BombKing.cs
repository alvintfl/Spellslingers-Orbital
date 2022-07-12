using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombKing : Enemy
{
    [SerializeField] private GameObject loot;

    public override void Awake()
    {
        base.Awake();
        Vector2 spawnPosition = new Vector2(-71, -53);
        gameObject.transform.position = spawnPosition;
        AudioManager.instance.Play("BombKingSpawn");
    }
    public override void Die()
    {
        AudioManager.instance.Play("BombKingDeath");
        GameObject loot = Instantiate(this.loot);
        loot.transform.position = this.transform.position;
        base.Die();
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
