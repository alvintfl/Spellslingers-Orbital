using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePillar : MonoBehaviour
{
    private int damage;
    private new Collider2D collider;

    private void Start()
    {
        this.damage = 30;
        this.collider = GetComponent<Collider2D>();
        this.transform.position = Player.instance.transform.position;
    }

    private void OnEnable()
    {
        this.transform.position = Player.instance.transform.position;
    }

    private void Activate()
    {
        this.collider.enabled = true;
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
