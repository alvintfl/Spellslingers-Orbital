using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that ensures the camera 
 * follows the player.
 * </summary>
 */
public class CameraFollow : MonoBehaviour
{
    private Vector3 tempPos;
    [SerializeField] private float minX, maxX, minY, maxY;

    void LateUpdate()
    {
        if (Player.instance.GetCurrentHealth() <= 0)
        {
            return;
        }
        tempPos = transform.position;
        tempPos.x = Player.instance.transform.position.x;
        tempPos.y = Player.instance.transform.position.y;
        transform.position = tempPos;
    }
}
