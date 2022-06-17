using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject LavaMapPrefab;
    [SerializeField] private GameObject SnowMapPrefab;
    [SerializeField] private GameObject AshMapPrefab;

    private void Start()
    {
        Instantiate(LavaMapPrefab);
        Instantiate(SnowMapPrefab);
        Instantiate(AshMapPrefab);
    }
}
