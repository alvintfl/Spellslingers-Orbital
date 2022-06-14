using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeEvent : MonoBehaviour
{
    void BossCockatriceSwipe()
    {
        if (!Player.instance.Avoidance.avoidRoll())
        {
            Player.instance.Health.TakeDamage(20f);
        }
    }
}
