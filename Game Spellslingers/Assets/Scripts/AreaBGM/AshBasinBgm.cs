using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshBasinBgm : MonoBehaviour
{
    void OnEnable()
    {
        AudioManager.instance.Play("bgm_ashbasin");
    }
    void OnDisable()
    {
        AudioManager.instance.Stop("bgm_ashbasin");
    }
}
