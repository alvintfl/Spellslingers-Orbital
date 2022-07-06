    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that applies slow.
 * </summary>
 */
public class PlayerSlow : MonoBehaviour
{
    [SerializeField] private Slow slow;

    public void Slow(Collider2D collision)
    {
        if (collision != null)
        {
            Character character = collision.GetComponent<Character>();
            character.StartHandleStatusEffect(this.slow);
        }
    }
}
