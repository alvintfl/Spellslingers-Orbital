using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningOrb : MonoBehaviour
{
    private static int count;
    private static GameObject northBall;
    private static float rotationSpeed = 100f;
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
        LightningOrb.count++;
        transform.SetParent(Camera.main.transform);
    }

    private void Update()
    {
        transform.RotateAround(Player.instance.transform.position, this.direction, LightningOrb.rotationSpeed * Time.deltaTime);
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
        switch (LightningOrb.count)
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
        LightningOrb.rotationSpeed *= 1.1f;
    }

    public static void Reset()
    {
        LightningOrb.count = 0;
        LightningOrb.northBall = null;
        LightningOrb.rotationSpeed = 100f;
        LightningOrb.Damage = 5f;
    }
}
