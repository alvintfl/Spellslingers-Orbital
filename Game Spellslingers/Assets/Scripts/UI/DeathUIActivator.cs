using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * <summary>
 * A class that keeps track 
 * of the player's deaths.
 * </summary>
 */
public class DeathUIActivator : MonoBehaviour
{
    [SerializeField]
    private GameObject dsc;
    private float played;

    void Start() 
    {
        Player.instance.Death += PlayerDiedListener;
        played = 0;
    }

    void OnDisable() 
    {
        Player.instance.Death -= PlayerDiedListener;
    }

    void PlayerDiedListener(Character sender, EventArgs e) 
    {
        string s = Player.instance.ToString();
        if (played < 1)
        {
            if (s == "Mage")
            {
                AudioManager.instance.Play("mage_death");
            }
            if (s == "Archer")
            {
                AudioManager.instance.Play("archer_death");
            }
            if (s == "Warrior")
            {
                AudioManager.instance.Play("warrior_death");
            }
        }
        if (played < 1)
        {
            played = 1;
        }
        dsc.SetActive(true);
    }
}
