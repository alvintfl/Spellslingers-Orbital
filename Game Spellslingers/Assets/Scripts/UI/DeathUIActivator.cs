using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * <summary>
 * A class that keeps track 
 * of events that can happen to
 * the player.
 * </summary>
 */
public class DeathUIActivator : MonoBehaviour
{
    [SerializeField]
    private GameObject dsc;

    void Start() 
    {
        // subscribe to function
        Player.instance.Health.DiedInfo += PlayerDiedListener;
    }

    void OnDisable() 
    {
        Player.instance.Health.DiedInfo -= PlayerDiedListener;
    }

    void PlayerDiedListener() 
    {
        print("Player has died.");
        dsc.SetActive(true);
    }
}
