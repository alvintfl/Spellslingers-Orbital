using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUIController : MonoBehaviour
{
    public static GameplayUIController instance;
    [SerializeField]
    private GameObject omc;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void OpenOptionsMenu()
    {
        omc.SetActive(true);
    }
}
