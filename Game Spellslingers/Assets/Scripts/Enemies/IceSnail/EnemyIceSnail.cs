using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/**
 * <summary>
 * A class that represents an ice snail.
 * </summary>
 */
public class EnemyIceSnail : Enemy
{
    private Animator anim;
    private new Collider2D collider;

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

    /**
     * <summary>
     * Determine if the player is close enough for the ice snail
     * to come out of it's shell.
     * </summary>
     */

    private void ToggleShell()
    {
        if ((gameObject.transform.position - Player.instance.transform.position).sqrMagnitude <= 20)
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
