using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable() 
    {
        // subscribe to function
        PlayerHealth.playerDiedInfo += PlayerDiedListener;
    }

    void OnDisable() 
    {
        PlayerHealth.playerDiedInfo -= PlayerDiedListener;
    }

    void PlayerDiedListener() 
    {
        print("Player has died.");
        
    }


}
