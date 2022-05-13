using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private int maxExp = 1;
    private int exp = 0;

    void Start()
    {
        this.slider.value = this.exp;    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddExp(1);
        }

        if (this.exp >= this.maxExp)
        {
            LevelUp();
        }    
    }

    public void LevelUp()
    {
        this.exp -= this.maxExp;
        this.maxExp += 1;
        slider.value = this.exp;
        slider.maxValue = this.maxExp;
    }

    public void AddExp(int exp)
    {
        this.exp += exp;
        slider.value += exp;
    }
    
}
