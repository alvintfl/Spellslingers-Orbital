using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * <summary>
 * A class for the player to 
 * manage their movement.
 * </summary>
 */
public class PlayerMovement : Movement
{
    private Animator anim;

    public override void Awake()
    {
        base.Awake();
        this.anim = GetComponent<Animator>();
    }

    public override void Update()
    {
        base.Update();
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        GetUserInput(x, y);
    }

    private void GetUserInput(float x, float y)
    {
        SetX(x);
        SetY(y);
        this.anim.SetFloat("Horizontal", x);
        this.anim.SetFloat("Vertical", y);
    }

    public override void AnimateMovement()
    {
        this.anim.SetFloat("Speed", GetDirection());
    }
}
