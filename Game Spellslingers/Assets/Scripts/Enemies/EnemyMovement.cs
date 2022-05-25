using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    public override void Update()
    {
        MoveToPlayer();
        base.Update();
    }

    private void MoveToPlayer()
    {
        Vector3 direction = GameObjectManager.instance.allObjects
            .Find(x => x.CompareTag("Player")).gameObject.transform.position - transform.position;
        // for rotation
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        SetX(direction.x);
        SetY(direction.y);  
    }
}
