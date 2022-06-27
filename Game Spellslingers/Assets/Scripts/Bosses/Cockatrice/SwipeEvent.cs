using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeEvent : MonoBehaviour
{
    void BossCockatriceSwipe()
    {
        Player.instance.TakeDamage(20f);
    }
}
