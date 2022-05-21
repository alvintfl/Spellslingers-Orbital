using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectAutoAdd : MonoBehaviour
{
    private void Awake() 
    {
        GameObjectManager.instance.allObjects.Add(gameObject); 
    }

    private void OnDestory()
    {
        GameObjectManager.instance.allObjects.Remove(gameObject);
    }
}
