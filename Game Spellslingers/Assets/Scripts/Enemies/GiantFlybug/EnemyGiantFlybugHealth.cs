using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGiantFlybugHealth : Health
{
    private bool isEnraged;

    private void Start()
    {
        isEnraged = false;
        AudioManager.instance.Play("giantflybug_calm");
    }

    private void Update()
    {
        if (isEnraged)
        {
            gameObject.GetComponent<EnemyMovement>().SetMoveSpeed(7);
        }
    }

    public override void TakeDamage(float damage)
    {
        AudioManager.instance.Stop("giantflybug_calm");
        base.TakeDamage(damage);
        isEnraged = true;
        AudioManager.instance.Play("giantflybug_enraged");
        anim.SetTrigger("Enraged");
    }
}
