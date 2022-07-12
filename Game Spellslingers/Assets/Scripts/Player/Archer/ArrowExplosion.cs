using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowExplosion : MonoBehaviour
{
    private new Collider2D collider;
    private void Start()
    {
        this.collider = GetComponent<Collider2D>();
    }

    public void Activate()
    {
        this.collider.enabled = true;
        AudioManager.instance.Play("ArrowExplosion");
    }

    private void Deactivate()
    {
        this.collider.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            collider.gameObject.GetComponent<Enemy>().TakeDamage(Arrow.Damage * 1.5f * Arrow.DamageMulti);
        }
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}
