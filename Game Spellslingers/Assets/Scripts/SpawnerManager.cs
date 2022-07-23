using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that manages spawners.
 * </summary>
 */
public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnerList;

    private int prev;
    private int curr;

    public GameObject activeSpawner;

    private void Start()
    {
        prev = 0;
        activeSpawner = Instantiate(spawnerList[curr]);
    }

    private void Update()
    {
        curr = Player.instance.FindCurrentLocation();
        if (curr != prev)
        {
            Destroy(activeSpawner);
            activeSpawner = Instantiate(spawnerList[curr]);
            prev = curr;
        }
    }
}
