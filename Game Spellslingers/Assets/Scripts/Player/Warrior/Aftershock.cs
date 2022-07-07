using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aftershock : MonoBehaviour
{
    private float damage;
    private float aoe;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("Aftershock");
        Warrior warrior = (Warrior)Player.instance;
        damage = warrior.GetAttack() * 1.5f;
        aoe = warrior.GetSlamArea();
        transform.localScale = new Vector2(10 + aoe, 10 + aoe);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
