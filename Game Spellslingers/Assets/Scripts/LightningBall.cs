using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBall : MonoBehaviour
{
    private float rotationSpeed;
    private Vector3 direction;

    private void Awake()
    {
        this.rotationSpeed = 100f;
        this.direction = new Vector3(0, 0, 1);
    }

    private void Update()
    {
        transform.RotateAround(Player.instance.transform.position, this.direction, this.rotationSpeed * Time.deltaTime);
    }
}
