using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    private float damage;
    private new Collider2D collider;
    private float directionMagnitude;

    private void Start()
    {
        this.damage = 10;
        this.collider = GetComponent<Collider2D>();
        this.collider.enabled = false;
        this.directionMagnitude = 3.5f;
        transform.SetParent(Camera.main.transform);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(this.damage);
        }
    }

    private void OnEnable()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = Player.instance.transform.position;
        Vector2 mouseDirection = mousePosition - playerPosition;
        mouseDirection.Normalize();
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg - 90;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
        gameObject.transform.position = playerPosition + mouseDirection * this.directionMagnitude;
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

    public void IncreaseRange()
    {
        gameObject.transform.localScale *= 1.1f;
        this.directionMagnitude += 0.25f;
    }

    public float Damage { get { return this.damage; } set { this.damage = value; } }
}
