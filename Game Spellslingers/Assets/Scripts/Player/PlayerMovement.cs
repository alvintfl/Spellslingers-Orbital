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
        GetUserInput();
        base.Update();
    }

    private void GetUserInput()
    {
        SetX(Input.GetAxisRaw("Horizontal"));
        SetY(Input.GetAxisRaw("Vertical"));
    }
}
