using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private HealthBar healthBar;
    private static int maxHealth = 100;

    Player() : base(10f, Player.maxHealth) { }

    public override void Awake()
    {
        base.Awake();
        this.healthBar.SetMaxHealth(Player.maxHealth);
    }

    void Update()
    {
        // Get user input 
        SetX(Input.GetAxisRaw("Horizontal"));
        SetY(Input.GetAxisRaw("Vertical"));
        AnimateMovement();
    }

    void FixedUpdate()
    {
        UpdatePosition();
    }
    /*
    private void OnCollisonEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(ENEMY_TAG)) {
            Debug.Log("hi");
            TakeDamage(10);
            Destroy(gameObject);
        }
    }*/

    public override void TakeDamage(int damage) {
        base.TakeDamage(damage);
        healthBar.SetHealth(CurrentHealth);
    }
}
