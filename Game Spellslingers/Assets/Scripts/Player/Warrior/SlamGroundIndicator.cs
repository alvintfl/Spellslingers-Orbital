using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamGroundIndicator : MonoBehaviour
{
    private SpriteRenderer sprRend;
    void OnEnable()
    {
        Warrior.slamAreaIncInfo += SetIndicatorSize;
    }

    void OnDisable()
    {
        Warrior.slamAreaIncInfo -= SetIndicatorSize;
    }


    void Start()
    {
        sprRend = gameObject.GetComponent<SpriteRenderer>();
    }

    void SetIndicatorSize(float increment)
    {
        sprRend.size += new Vector2(2 * increment, 2* increment);
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
