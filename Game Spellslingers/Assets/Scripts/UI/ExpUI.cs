using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private ExpManager expManager;

    private void Start()
    {
        this.slider.value = 0;
        this.slider.maxValue = 1;
        this.expManager.GainMaxExp += UpdateMaxExp;
        this.expManager.GainExp += UpdateExp;
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
