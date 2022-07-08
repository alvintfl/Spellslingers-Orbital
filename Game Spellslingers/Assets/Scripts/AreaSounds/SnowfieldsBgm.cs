using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowfieldsBgm : MonoBehaviour
{
    void OnEnable()
    {
        AudioManager.instance.Play("bgm_snowfields");
        AudioManager.instance.Play("snowfields_winds");
    }
    void OnDisable()
    {
        AudioManager.instance.Stop("bgm_snowfields");
        AudioManager.instance.Stop("snowfields_winds");
    }
}
