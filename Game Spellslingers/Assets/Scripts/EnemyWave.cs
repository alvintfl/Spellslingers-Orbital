using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that represents an 
 * enemy wave.
 * </summary>
 */
public class EnemyWave : MonoBehaviour
{
    /**
     * <summary>
     * The level that the player can fight this 
     * wave of enemies.
     * </summary>
     */
    [SerializeField] private int level;

    /**
     * <summary>
     * The enemies in this enemy wave.
     * </summary>
     */
    [SerializeField] private List<GameObject> enemies;

    public int Level { get { return this.level; } }

    public List<GameObject> Enemies { get { return this.enemies; } }
}
