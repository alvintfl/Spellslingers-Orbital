using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoBgm : MonoBehaviour
{
    void OnEnable()
    {
        AudioManager.instance.Play("bgm_volcano");
        AudioManager.instance.Play("volcano_smoke");
    }
    void OnDisable()
    {
        AudioManager.instance.Stop("bgm_volcano");
        AudioManager.instance.Stop("volcano_smoke");
    }
}
