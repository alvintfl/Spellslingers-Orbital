using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private HealthBar healthBar;
    private static int maxHealth = 50;

    private Transform player;
    private Vector2 enemyMove;
    private string PLAYER_TAG = "Player";

    Enemy() : base(2f, Enemy.maxHealth) { }

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindWithTag(PLAYER_TAG).transform;
    }

    // Update is called once per frame
    void Update() {
        Vector3 direction = player.position - transform.position;
        // for rotation
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        enemyMove = direction;
    }
    void FixedUpdate() {
        moveEnemy(enemyMove);
    }


    void moveEnemy(Vector2 direction) {
        this.GetRb().MovePosition((Vector2)transform.position + (direction * this.GetMoveSpeed() * Time.deltaTime));
    }

}
