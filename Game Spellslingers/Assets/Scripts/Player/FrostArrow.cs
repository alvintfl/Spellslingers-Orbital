using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostArrow : MonoBehaviour
{
    [SerializeField] private Slow slowPrefab;

    public void Slow(Collider2D collision)
    {
        Slow slow = Instantiate(slowPrefab).GetComponent<Slow>();
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
