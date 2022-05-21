using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable() {
        // subscribe to function
        Player.playerDiedInfo += PlayerDiedListener;
    }

    void OnDisable() {
        Player.playerDiedInfo -= PlayerDiedListener;
    }

    void PlayerDiedListener() 
    {
        print("Player died.");
    }


}
