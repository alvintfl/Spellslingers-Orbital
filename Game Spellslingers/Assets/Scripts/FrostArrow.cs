using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostArrow : MonoBehaviour
{
    private Slow slow;

    private void Start()
    {
        this.slow = gameObject.GetComponent<Slow>();
    }

    public void Slow(Collider2D collision)
    {
        StartCoroutine(
            collision.gameObject.GetComponent<Character>().HandleStatusEffect(this.slow));
    }
}
