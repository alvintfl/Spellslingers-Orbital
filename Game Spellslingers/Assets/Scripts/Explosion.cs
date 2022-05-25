using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private int damage = 30;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Triggered " + collider.gameObject.name);
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("EXPLOSION");
            Player.instance.Health.TakeDamage(this.damage);
        }
    }
}
