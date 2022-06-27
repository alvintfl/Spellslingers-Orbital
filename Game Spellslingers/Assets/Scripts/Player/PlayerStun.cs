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
            try
            {
                StartCoroutine(collision.gameObject
                    .GetComponent<Character>().HandleStatusEffect(stun));
            }
            catch (MissingReferenceException)
            {
                Debug.Log("Enemy died before stun activated.");
            }
        }
    }
}
