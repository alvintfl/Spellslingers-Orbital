using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Vector3 tempPos;
    [SerializeField]
    private float minX, maxX, minY, maxY;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    
    void LateUpdate()
    {
        if (Player.instance.Health.CurrentHealth <= 0) {
            return;
        }
        tempPos = transform.position;
        tempPos.x = Player.instance.transform.position.x;
        tempPos.y = Player.instance.transform.position.y;
        /*
        if (tempPos.x < minX) {
            tempPos.x = minX;
        }
        if (tempPos.x > maxX) {
            tempPos.x = maxX;
        }
        if (tempPos.y < minY) {
            tempPos.y = minY;
        }
        if (tempPos.y > maxY) {
            tempPos.y = maxY;
        }
        */
        transform.position = tempPos;
    }
    
} // class
