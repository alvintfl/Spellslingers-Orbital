using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private HealthBar healthBar;
    public static int maxHealth = 100;

    //private string ENEMY_TAG = "Enemy";

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
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        Debug.Log("????");
        if (collision.gameObject.CompareTag(ENEMY_TAG)) {
            Debug.Log("hi");
            TakeDamage(10);
        }
    }*/

    public override void TakeDamage(float damage) 
    {
        base.TakeDamage(damage);
        healthBar.SetHealth(CurrentHealth);
    }

    public void SetHealth(float health) 
    {
        CurrentHealth = health;
        this.healthBar.SetHealth(health);
    }
}
