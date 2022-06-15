using System;
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
        Player.instance.Death += PlayerDiedListener;
    }

    void OnDisable() 
    {
        Player.instance.Death -= PlayerDiedListener;
    }

    void PlayerDiedListener(Character sender, EventArgs e) 
    {
        print("Player has died.");
        dsc.SetActive(true);
    }
}
