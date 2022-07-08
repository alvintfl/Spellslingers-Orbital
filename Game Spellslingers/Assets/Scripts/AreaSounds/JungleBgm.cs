using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JungleBgm : MonoBehaviour
{
    void OnEnable()
    {
        AudioManager.instance.Play("bgm_jungle");
        AudioManager.instance.Play("jungle_rain");
    }
    void OnDisable()
    {
        AudioManager.instance.Stop("bgm_jungle");
        AudioManager.instance.Stop("jungle_rain");
    }
}
