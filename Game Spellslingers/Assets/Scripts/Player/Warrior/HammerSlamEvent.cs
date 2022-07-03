using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSlamEvent : MonoBehaviour
{
    public delegate void SlamEvent();
    public static event SlamEvent slamEventInfo;
    void ActivateSlamEvent()
    {
        if (slamEventInfo != null)
        {
            slamEventInfo();
        }
    }
}
