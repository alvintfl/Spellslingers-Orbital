using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyIceSnail : Enemy
{
    private Animator anim;
    private new Collider2D collider;

    public EnemyIceSnail() : base(15, 10) { }

    public override void Start()
    {
        base.Start();
        this.anim = GetComponent<Animator>();
        this.collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        ToggleShell();
    }

    private void ToggleShell()
    {
        if ((gameObject.transform.position - Player.instance.transform.position).sqrMagnitude <= 17.5)
        {
            this.anim.SetBool("Hide", false);
            this.collider.enabled = true;
        } else
        {
            this.anim.SetBool("Hide", true);
            this.collider.enabled = false;
        }
    }
}
