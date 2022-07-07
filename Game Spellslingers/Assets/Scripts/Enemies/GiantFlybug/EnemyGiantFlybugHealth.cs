using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGiantFlybugHealth : Health
{
    private bool isEnraged;
    private Animator anim;

    private void Start()
    {
        isEnraged = false;
        anim = gameObject.GetComponent<Animator>();
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
        base.TakeDamage(damage);
        isEnraged = true;
        anim.SetTrigger("Enraged");
    }


}
