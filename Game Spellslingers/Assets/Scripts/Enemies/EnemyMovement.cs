using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    private Transform player;
    private Vector2 enemyMove;
    public string PLAYER_TAG = "Player";
    public EnemyMovement() : base(2f) { }

    private void Start()
    {
        player = GameObject.FindWithTag(PLAYER_TAG).transform;
    }

    public override void Update()
    {
        base.Update();
        Vector3 direction = player.position - transform.position;
        // for rotation
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        enemyMove = direction;
    }

    public override void FixedUpdate()
    {
        moveEnemy(enemyMove);
    }
    void moveEnemy(Vector2 direction) {
        this.GetRb().MovePosition((Vector2)transform.position + (direction * this.GetMoveSpeed() * Time.deltaTime));
    }
}
