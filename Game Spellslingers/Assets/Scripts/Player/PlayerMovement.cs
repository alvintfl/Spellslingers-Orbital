using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    public PlayerMovement() : base(6f) { }
    public override void Update()
    {
        // Get user input 
        SetX(Input.GetAxisRaw("Horizontal"));
        SetY(Input.GetAxisRaw("Vertical"));
        base.Update();
    }
}
