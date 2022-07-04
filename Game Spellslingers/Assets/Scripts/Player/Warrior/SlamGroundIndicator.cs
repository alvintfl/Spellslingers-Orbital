using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamGroundIndicator : MonoBehaviour
{
    private SpriteRenderer sprRend;



    void Start()
    {
        sprRend = gameObject.GetComponent<SpriteRenderer>();
        Warrior warrior = (Warrior)Player.instance;
        float aoe = warrior.GetSlamArea();
        sprRend.size = new Vector2(aoe * 4, aoe * 4);
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
