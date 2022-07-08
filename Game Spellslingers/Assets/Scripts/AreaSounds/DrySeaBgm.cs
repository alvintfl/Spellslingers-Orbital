using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrySeaBgm : MonoBehaviour
{
    void OnEnable()
    {
        AudioManager.instance.Play("bgm_drysea");
        AudioManager.instance.Play("drysea_sandstorm");
    }
    void OnDisable()
    {
        AudioManager.instance.Stop("bgm_drysea");
        AudioManager.instance.Stop("drysea_sandstorm");
    }
}
