using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that represents an explosion.
 * </summary>
 */
public class Explosion : MonoBehaviour
{
    private int damage = 20;

    private new Collider2D collider;

    private void Start()
    {
        this.collider = GetComponent<Collider2D>();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Player.instance.TakeDamage(this.damage);
        }
    }

    public void Activate()
    {
        this.collider.enabled = true;
        AudioManager.instance.Play("Explosion");
    }

    private void Deactivate()
    {
        this.collider.enabled = false;
    }
}
