using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that manages the weather.
 * </summary>
 */
public class WeatherController : MonoBehaviour
{
    private int updateLocation;
    private int prevLocation;

    void Start()
    {
        prevLocation = 0;
    }
    // Update is called once per frame
    void Update()
    {
        updateLocation = Player.instance.FindCurrentLocation();
        if (prevLocation != updateLocation)
        {
            if (!transform.GetChild(updateLocation).gameObject.activeSelf)
            {
                transform.GetChild(prevLocation).gameObject.SetActive(false);
                transform.GetChild(updateLocation).gameObject.SetActive(true);
                prevLocation = updateLocation;
            }
        }

    }
}
