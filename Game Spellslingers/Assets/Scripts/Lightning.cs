using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    private int damage;
    private new Collider2D collider;

    private void Start()
    {
        this.damage = 10;
        this.collider = GetComponent<Collider2D>();
        this.collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(this.damage);
        }
    }

    private void Activate()
    {
        this.collider.enabled = true;
    }

    private void Stop()
    {
        this.collider.enabled = false;
        gameObject.SetActive(false);
    }
}
