using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyIceSnail : Enemy
{
    private Animator anim;
    private new EdgeCollider2D collider;
    /*
    private new BoxCollider2D collider;
    private Vector2 shellOffset;
    private Vector2 shellSize;
    private Vector2 normalOffset;
    private Vector2 normalSize;
    */

    public EnemyIceSnail() : base(15, 10) { }

    public override void Start()
    {
        base.Start();
        this.anim = GetComponent<Animator>();
        this.collider = GetComponent<EdgeCollider2D>();
        /*
        this.collider = GetComponent<BoxCollider2D>();
        this.normalOffset = new Vector2(0.219f, 0.006f);
        this.normalSize = new Vector2(1.783f, 1.354f);
        this.shellOffset = new Vector2(-0.033f, 0.017f);
        this.shellSize = new Vector2(1.117f, 1.33f);
        */
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
            /*
            this.collider.offset = this.normalOffset;
            this.collider.size = this.normalSize;
            */
        } else
        {
            this.anim.SetBool("Hide", true);
            this.collider.enabled = false;
            /*
            this.collider.offset = this.shellOffset;
            this.collider.size = this.shellSize;
            */
        }
    }
}
