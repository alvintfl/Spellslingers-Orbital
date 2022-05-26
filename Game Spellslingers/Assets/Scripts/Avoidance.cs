using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that is responsible for avoidance.
 * </summary>
 */
public class Avoidance : MonoBehaviour
{
    private int avoidChance = 0;
    public Avoidance(int chance)
    {
        this.avoidChance = chance;
    }

    public int getAvoidChance() 
    {
        return avoidChance;
    }
    public void setAvoidChance(int value) 
    {
        avoidChance = value;
    }

    public bool avoidRoll()
    {
        int roll = (int)Random.Range(1f, 100f);
        print("roll is " + roll);
        print("avoid chance is " + avoidChance);
        if (roll <= avoidChance)
        {
            print("damage avoided");
            return true;
        }
        print("damage not avoided");
        return false;
    }
}
