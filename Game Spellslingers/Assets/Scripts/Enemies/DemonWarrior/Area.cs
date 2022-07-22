using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    void Start()
    {
        Warrior warrior = (Warrior)Player.instance;
        float aoe = warrior.GetSlamArea();
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
