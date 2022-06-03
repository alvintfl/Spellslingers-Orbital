using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * <summary>
 * A class representing the
 * health bar.
 * </summary>
 */
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Update()
    {
        this.slider.transform.rotation = Quaternion.identity;
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
