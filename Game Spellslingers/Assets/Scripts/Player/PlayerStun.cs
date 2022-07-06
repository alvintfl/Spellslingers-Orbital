using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    [SerializeField] private Stun stun;

    public void Stun(Collider2D collision)
    {
        if (collision != null)
        {
            Character character = collision.GetComponent<Character>();
            character.StartHandleStatusEffect(this.stun);
        }
    }
}
