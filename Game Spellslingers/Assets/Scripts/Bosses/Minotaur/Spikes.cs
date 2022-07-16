using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private int damage;
    private new Collider2D collider;

    private void Start()
    {
        this.damage = 30;
        this.collider = GetComponent<Collider2D>();
    }

    private void Activate()
    {
        this.collider.enabled = true;
        AudioManager.instance.Play("Spikes");
    }

    private void Deactivate()
    {
        this.collider.enabled = false;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Player.instance.TakeDamage(this.damage);
        }
    }
}
