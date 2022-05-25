using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
