using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PincerGrasp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Player.instance.TakeDamage(40f);
        }
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
