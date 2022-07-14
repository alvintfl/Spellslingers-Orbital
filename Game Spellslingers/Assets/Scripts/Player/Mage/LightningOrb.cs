using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningOrb : MonoBehaviour
{
    public static int Count { get; private set; } = 0;
    private static GameObject northBall;
    public static float RotationSpeed { get; private set; } = 100f;
    private static int magnitude = 4;
    public static float Damage { get; set; } = 5f;
    private Vector3 direction;

    private enum Position
    {
        North, South, East, West,
        NorthEast, NorthWest, SouthEast, SouthWest
    }

    private void Awake()
    {
        this.direction = new Vector3(0, 0, 1);
        SetPosition();
        LightningOrb.Count++;
        transform.SetParent(Camera.main.transform);
        Player.instance.Death += Destroy;
    }

    private void Update()
    {
        transform.RotateAround(Player.instance.transform.position, this.direction, LightningOrb.RotationSpeed * Time.deltaTime);
    }

    private void Destroy(Character sender, EventArgs e)
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Player.instance.Death -= Destroy;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(LightningOrb.Damage);
        }
    }

    private void SetPosition()
    {
        Vector2 playerPosition = Player.instance.transform.position;
        switch (LightningOrb.Count)
        {
            case (int)Position.North:
                gameObject.transform.position = playerPosition + new Vector2(0, LightningOrb.magnitude);
                LightningOrb.northBall = gameObject;
                break;
            case (int)Position.South:
                Vector2 northBallPosition = LightningOrb.northBall.transform.position;
                Vector2 southDirection = playerPosition - northBallPosition;
                gameObject.transform.position = playerPosition + southDirection;
                break;
            case (int)Position.East:
                northBallPosition = LightningOrb.northBall.transform.position;
                Vector2 eastDirection = Quaternion.Euler(0, 0, 90) * (playerPosition - northBallPosition);
                gameObject.transform.position = playerPosition + eastDirection;
                break;
            case (int)Position.West:
                northBallPosition = LightningOrb.northBall.transform.position;
                Vector2 westDirection = Quaternion.Euler(0, 0, -90) * (playerPosition - northBallPosition);
                gameObject.transform.position = playerPosition + westDirection;
                break;
        }
    }

    public static void IncreaseRotationSpeed()
    {
        LightningOrb.RotationSpeed *= 1.1f;
    }

    public static void Reset()
    {
        LightningOrb.Count = 0;
        LightningOrb.northBall = null;
        LightningOrb.RotationSpeed = 100f;
        LightningOrb.Damage = 5f;
    }
}
