using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshBasinBgm : MonoBehaviour
{
    void OnEnable()
    {
        AudioManager.instance.Play("bgm_ashbasin");
        AudioManager.instance.Play("ashbasin_ashfall");
    }
    void OnDisable()
    {
        AudioManager.instance.Stop("bgm_ashbasin");
        AudioManager.instance.Stop("ashbasin_ashfall");
    }
}
