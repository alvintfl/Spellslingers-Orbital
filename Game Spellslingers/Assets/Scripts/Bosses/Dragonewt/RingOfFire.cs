using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOfFire : MonoBehaviour
{
    private int damage;
    private new Collider2D collider;

    private void Start()
    {
        this.damage = 0;
        this.collider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (!Player.instance.AvoidRoll())
            {
                Player.instance.TakeDamage(this.damage);
            }
        }
    }

}
