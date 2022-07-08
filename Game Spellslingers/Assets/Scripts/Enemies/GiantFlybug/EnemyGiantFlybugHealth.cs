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
        AudioManager.instance.Play("giantflybug_calm");
    }
    private void OnDisable()
    {
        AudioManager.instance.Stop("giantflybug_enraged");
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
