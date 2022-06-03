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
        SetAnimParam("Horizontal", x);
        SetAnimParam("Vertical", y);
    }
}
