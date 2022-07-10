using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombKing : Enemy
{
    public override void Awake()
    {
        base.Awake();
    }
    public override void Die()
    {
        /*
        AudioManager.instance.Play("DragonewtDeath");
        GameObject loot = Instantiate(this.loot);
        loot.transform.position = this.transform.position;
        */
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
