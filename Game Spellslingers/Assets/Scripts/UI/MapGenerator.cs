using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject LavaMapPrefab;

    private void Start()
    {
        Instantiate(LavaMapPrefab);
    }
}
