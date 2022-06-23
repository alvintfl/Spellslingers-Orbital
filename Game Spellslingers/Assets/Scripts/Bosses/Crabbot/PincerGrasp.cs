using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PincerGrasp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (!Player.instance.AvoidRoll())
            {
                Player.instance.TakeDamage(20f);
            }
        }
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
