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
    /*
    [SerializeField] private float 
        minXCentre, maxXCentre, minYCentre, maxYCentre,
        minXInBetween, maxXInBetween, minYInBetween, maxYInBetween,
        minX, maxX, minY, maxY;
    */

    void LateUpdate()
    {
        if (Player.instance.GetCurrentHealth() <= 0)
        {
            return;
        }
        tempPos = transform.position;
        tempPos.x = Player.instance.transform.position.x;
        tempPos.y = Player.instance.transform.position.y;

        /*
        float playerX = Player.instance.transform.position.x;
        float playerY = Player.instance.transform.position.y;

        if (playerX >= this.maxX)
        {
            tempPos.x = this.maxX;
            if (playerY >= this.maxYCentre)
            {
                tempPos.y = this.maxYCentre;
            } else if (playerY <= this.minYCentre)
            {
                tempPos.y = this.minYCentre;
            } else
            {
                tempPos.y = playerY;
            }
        } else if (playerX <= this.minX){
            tempPos.x = this.minX;
            if (playerY >= this.maxYCentre)
            {
                tempPos.y = this.maxYCentre;
            } else if (playerY <= this.minYCentre)
            {
                tempPos.y = this.minYCentre;
            } else
            {
                tempPos.y = playerY;
            }
        } else if (playerY >= this.maxY)
        {
            tempPos.y = this.maxY;
            if (playerX >= this.maxXCentre)
            {
                tempPos.x = this.maxXCentre;
            } else if (playerX <= this.minXCentre)
            {
                tempPos.x = this.minXCentre;
            } else
            {
                tempPos.x = playerX;
            }
        } else if (playerY <= this.minY)
        {
            tempPos.y = this.minY;
            if (playerX >= this.maxXCentre)
            {
                tempPos.x = this.maxXCentre;
            } else if (playerX <= this.minXCentre)
            {
                tempPos.x = this.minXCentre;
            } else
            {
                tempPos.x = playerX;
            }
        } else
        {
            if (playerX <= this.minXInBetween)
            {
                if (playerY >= this.maxYCentre)
                {
                    tempPos.y = this.maxYCentre;
                } else if (playerY <= this.minYCentre)
                {
                    tempPos.y = this.minYCentre;
                } else
                {
                    tempPos.y = playerY;
                }
                tempPos.x = playerX;
            } else if (playerX >= this.maxXInBetween)
            {
                if (playerY >= this.maxYCentre)
                {
                    tempPos.y = this.maxYCentre;
                } else if (playerY <= this.minYCentre)
                {
                    tempPos.y = this.minYCentre;
                } else
                {
                    tempPos.y = playerY;
                }
                tempPos.x = playerX;
            } else
            {
                if (playerX >= this.maxXCentre)
                {
                    if (playerY >= this.maxYInBetween || playerY <= this.minYInBetween)
                    {
                        tempPos.x = this.maxXCentre;
                    } else
                    {
                        tempPos.x = playerX;
                    }
                    tempPos.y = playerY;
                } else if (playerX <= this.minXCentre)
                {
                    if (playerY >= this.maxYInBetween || playerY <= this.minYInBetween)
                    {
                        tempPos.x = this.minXCentre;
                    } else
                    {
                        tempPos.x = playerX;
                    }
                    tempPos.y = playerY;
                } else
                {
                    tempPos.x = playerX;
                    tempPos.y = playerY;
                }
            }
        }
        */
        transform.position = tempPos;
    }
}
