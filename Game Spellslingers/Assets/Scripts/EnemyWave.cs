using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private List<GameObject> enemies;

    public int Level { get { return this.level; } }

    public List<GameObject> Enemies { get { return this.enemies; } }
}
