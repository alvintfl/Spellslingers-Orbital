using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCockatrice : Enemy
{
    // to do:
    /*
     * make cockatrice move to player, but not too close
     * create projectiles for claw cast, storm call, frost breath/ice beam
     * 
     */

    public override void Die()
    {
        // play death animation
        Destroy(gameObject);
    }
}
