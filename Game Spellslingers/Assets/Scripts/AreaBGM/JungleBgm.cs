using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JungleBgm : MonoBehaviour
{
    void OnEnable()
    {
        AudioManager.instance.Play("bgm_jungle");
    }
    void OnDisable()
    {
        AudioManager.instance.Stop("bgm_jungle");
    }
}
