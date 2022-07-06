using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneShield : MonoBehaviour
{
    private void Awake()
    {
        gameObject.transform.SetParent(Camera.main.transform);
        gameObject.transform.position = Player.instance.transform.position;
    }
}
