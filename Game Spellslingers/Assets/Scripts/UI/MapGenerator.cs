using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> maps;

    private void Start()
    {
        foreach (GameObject map in maps)
        {
            Instantiate(map);
        }
    }
}
