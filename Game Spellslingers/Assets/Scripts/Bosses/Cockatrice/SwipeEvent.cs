using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeEvent : MonoBehaviour
{
    void BossCockatriceSwipe()
    {
        if (!Player.instance.AvoidRoll())
        {
            Player.instance.TakeDamage(20f);
        }
    }
}
