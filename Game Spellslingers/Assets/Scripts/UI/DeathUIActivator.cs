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
        if (Player.instance.playerClass == "Mage")
        {
            AudioManager.instance.Play("mage_death");
        }
        if (Player.instance.playerClass == "Archer")
        {
            AudioManager.instance.Play("archer_death");
        }
        if (Player.instance.playerClass == "Warrior")
        {
            AudioManager.instance.Play("warrior_death");
        }
        print("Player has died.");
        dsc.SetActive(true);
    }
}
