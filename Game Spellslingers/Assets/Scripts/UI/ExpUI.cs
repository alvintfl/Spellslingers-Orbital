using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * <summary>
 * A class resposible for the 
 * UI for the exp bar.
 * </summary>
 */
public class ExpUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private ExpManager expManager;

    private void Start()
    {
        this.slider.value = this.expManager.Exp;
        this.slider.maxValue = this.expManager.MaxExp;
        this.expManager.GainMaxExp += UpdateMaxExp;
        this.expManager.GainExp += UpdateExp;
    }

    private void OnDisable()
    {
        this.expManager.GainMaxExp -= UpdateMaxExp;
        this.expManager.GainExp -= UpdateExp;
    }

    public void UpdateMaxExp(object sender, EventArgs e)
    {
        this.slider.maxValue = this.expManager.MaxExp;
        this.slider.value = this.expManager.Exp;
    }

    public void UpdateExp(object sender, EventArgs e)
    {
        this.slider.value = this.expManager.Exp;
    }
}
