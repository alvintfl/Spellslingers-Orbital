using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



























    /*
    [SerializeField] private List<GameObject> spawnerList;



    private void Start()
    {
        ActivateSpawner();
        ExpManager.LevelUp += ActivateSpawner;
    }

    private void OnDestroy()
    {
        ExpManager.LevelUp -= ActivateSpawner;
    }
    
    private void ActivateSpawner(ExpManager sender, EventArgs e)
    {
        if (this.spawnerList.Count != 0)
        {
            GameObject spawnerObject = this.spawnerList[0];
            Spawner spawner = spawnerObject.GetComponent<Spawner>();
            if (sender.Level == spawner.StartLevel)
            {
                Instantiate(spawner);
                this.spawnerList.RemoveAt(0);
            }
        }
    }

    private void ActivateSpawner()
    {
        if (this.spawnerList.Count != 0)
        {
            Instantiate(this.spawnerList[0]);
            this.spawnerList.RemoveAt(0);
        }
    }
    */
}
