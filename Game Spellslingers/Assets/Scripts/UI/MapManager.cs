using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject startingMapPrefab;
    [SerializeField] private List<GameObject> mapsPrefab;
    private List<GameObject> maps;

    private enum Map
    {
        Barrier = 0
    }

    private void Start()
    {
        Instantiate(this.startingMapPrefab);
        this.maps = new List<GameObject>();
        Item.PickUp += AccessNewZone;
        foreach (GameObject mapPrefab in mapsPrefab)
        {
            GameObject map = Instantiate(mapPrefab);
            this.maps.Add(map);
        }
    }

    private void OnDestroy()
    {
        Item.PickUp -= AccessNewZone;
    }

    private void AccessNewZone(Item item, EventArgs e)
    {
        if (this.maps.Count != 0)
        {
            GameObject map = this.maps[0];
            map.GetComponentsInChildren<TilemapCollider2D>()[(int) Map.Barrier].enabled = false;
            this.maps.RemoveAt(0);
        }
    }
}
