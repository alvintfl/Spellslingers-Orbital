using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrySeaBgm : MonoBehaviour
{
    void OnEnable()
    {
        AudioManager.instance.Play("bgm_drysea");
    }
    void OnDisable()
    {
        AudioManager.instance.Stop("bgm_drysea");
    }
}
