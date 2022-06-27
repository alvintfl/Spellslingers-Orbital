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
            try
            {
                StartCoroutine(collision.gameObject
                    .GetComponent<Character>().HandleStatusEffect(slow));
            } catch(MissingReferenceException)
            {
                Debug.Log("Enemy died before slow activated.");
            }
        }
    }
}
