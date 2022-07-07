using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoBgm : MonoBehaviour
{
    void OnEnable()
    {
        AudioManager.instance.Play("bgm_volcano");
    }
    void OnDisable()
    {
        AudioManager.instance.Stop("bgm_volcano");
    }
}
